using System;
using _Skycall.Scripts.Enemies.Asteroid;
using _Skycall.Scripts.Enemies.EnemyShip;
using _Skycall.Scripts.GameStateMachine;
using _Skycall.Scripts.Helpers;
using _Skycall.Scripts.Player.Ship;
using _Skycall.Scripts.Player.Ship.States;
using UnityEngine;
using Zenject;

namespace _Skycall.Scripts.DI.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [Inject] Settings _settings = null;

        public override void InstallBindings()
        {
            InstallGameLogic();
            InstallEnemies();
            InstallShip();
            InstallHelpers();
        }

        private void InstallGameLogic()
        {
            Container.Bind<GameManager>().AsSingle();
        }

        private void InstallEnemies()
        {
            Container.Bind<AsteroidManager>()
                .FromComponentInNewPrefab(_settings.asteroidManager)
                .AsSingle();
            Container.Bind<EnemyShipManager>()
                .FromComponentInNewPrefab(_settings.enemyShipManager)
                .AsSingle();

            Container.BindFactory<Asteroid, Asteroid.Factory>()
                // This means that any time Asteroid.Factory.Create is called, it will instantiate
                // this prefab and then search it for the Asteroid component
                .FromComponentInNewPrefab(_settings.asteroidPrefab)
                // We can also tell Zenject what to name the new gameobject here
                .WithGameObjectName("Asteroid")
                // GameObjectGroup's are just game objects used for organization
                // This is nice so that it doesn't clutter up our scene hierarchy
                .UnderTransformGroup("Asteroids");

            Container.BindFactory<EnemyShip, EnemyShip.Factory>()
                .FromComponentInNewPrefab(_settings.enemyShipPrefab)
                .WithGameObjectName("EnemyShips")
                .UnderTransformGroup("EnemyShips");
        }

        void InstallShip()
        {
            Container.Bind<ShipStateFactory>().AsSingle();

            // Note that the ship itself is bound using a ZenjectBinding component (see Ship
            // game object in scene heirarchy)

            Container.BindFactory<ShipStateWaitingToStart, ShipStateWaitingToStart.Factory>()
                .WhenInjectedInto<ShipStateFactory>();
            Container.BindFactory<ShipStateDead, ShipStateDead.Factory>().WhenInjectedInto<ShipStateFactory>();
            Container.BindFactory<ShipStateMoving, ShipStateMoving.Factory>().WhenInjectedInto<ShipStateFactory>();
        }

        void InstallHelpers()
        {
            Container.Bind<LevelHelper>().AsSingle();
        }


        [Serializable]
        public class Settings
        {
            public GameObject asteroidPrefab;
            public GameObject enemyShipPrefab;
            public GameObject asteroidManager;
            public GameObject enemyShipManager;
        }
    }
}