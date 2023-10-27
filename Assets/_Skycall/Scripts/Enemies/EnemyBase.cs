using _Skycall.Scripts.Helpers;
using UnityEngine;
using Zenject;

namespace _Skycall.Scripts.Enemies
{
    public abstract class EnemyBase : MonoBehaviour
    {
        private LevelHelper _level;

        public virtual void FixedUpdateEnemy()
        {
        }

        public virtual void UpdateEnemy()
        {
        }

        protected abstract void CheckForTeleport();


        public class Factory : PlaceholderFactory<EnemyBase>
        {
        }
    }
}