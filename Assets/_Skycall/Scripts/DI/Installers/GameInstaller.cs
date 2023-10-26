using System;
using _Skycall.Scripts.Enemies.Asteroid;
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
        }

        private void InstallEnemies()
        {
            
            
            Container.BindFactory<Asteroid, Asteroid.Factory>()
                // This means that any time Asteroid.Factory.Create is called, it will instantiate
                // this prefab and then search it for the Asteroid component
                .FromComponentInNewPrefab(_settings.AsteroidPrefab)
                // We can also tell Zenject what to name the new gameobject here
                .WithGameObjectName("Asteroid")
                // GameObjectGroup's are just game objects used for organization
                // This is nice so that it doesn't clutter up our scene hierarchy
                .UnderTransformGroup("Asteroids");
        }
    }

    [Serializable]
    public class Settings
    {
        public GameObject ExplosionPrefab;
        public GameObject BrokenShipPrefab;
        public GameObject AsteroidPrefab;
        public GameObject ShipPrefab;
    }
}