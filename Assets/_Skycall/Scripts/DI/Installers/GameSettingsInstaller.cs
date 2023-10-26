using System;
using _Skycall.Scripts.Enemies.EnemyShip;
using _Skycall.Scripts.Player.Ship.States;
using UnityEngine;
using Zenject;
using Asteroid = _Skycall.Scripts.Enemies.Asteroid.Asteroid;
using AsteroidManager = _Skycall.Scripts.Enemies.Asteroid.AsteroidManager;

namespace _Skycall.Scripts.DI.Installers
{
    [CreateAssetMenu(menuName = "Skycall/Game Settings")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        public ShipSettings Ship;

        public AsteroidSettings asteroid;
        public EnemyShipSettings enemyShip;
        public GameInstaller.Settings gameInstaller;

        // We use nested classes here to group related settings together (Extenject)
        [Serializable]
        public class ShipSettings
        {
            public ShipStateMoving.Settings StateMoving;
            public ShipStateWaitingToStart.Settings StateStarting;
        }
        [Serializable]
        public class AsteroidSettings
        {
            public AsteroidManager.Settings spawner;
            public Asteroid.Settings general;
        }

        [Serializable]
        public class EnemyShipSettings
        {
            public EnemyShipManager.Settings spawner;
            public EnemyShip.Settings general;
        }


        public override void InstallBindings()
        {
            Container.BindInstance(Ship.StateMoving);
            Container.BindInstance(Ship.StateStarting);
            Container.BindInstance(asteroid.spawner);
            Container.BindInstance(asteroid.general);
            Container.BindInstance(enemyShip.spawner);
            Container.BindInstance(enemyShip.general);
            Container.BindInstance(gameInstaller);
        }
    }
}