using System;
using _Skycall.Scripts.Enemies.Asteroid;
using _Skycall.Scripts.Enemies.EnemyShip;
using _Skycall.Scripts.Helpers;
using UnityEngine;
using Zenject;

namespace _Skycall.Scripts.DI.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [Inject] Settings _settings = null;

        public override void InstallBindings()
        {
            InstallEnemies();
            InstallHelpers();
        }

        private void InstallEnemies()
        {
            Container.Bind<AsteroidManager>().AsSingle();
            Container.Bind<EnemyShipManager>().AsSingle();

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
                // This means that any time Asteroid.Factory.Create is called, it will instantiate
                // this prefab and then search it for the Asteroid component
                .FromComponentInNewPrefab(_settings.enemyShipPrefab)
                // We can also tell Zenject what to name the new gameobject here
                .WithGameObjectName("EnemyShips")
                // GameObjectGroup's are just game objects used for organization
                // This is nice so that it doesn't clutter up our scene hierarchy
                .UnderTransformGroup("EnemyShips");
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
        }
    }
}