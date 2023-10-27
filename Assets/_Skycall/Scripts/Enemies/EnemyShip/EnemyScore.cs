using _Skycall.Scripts.Level.Collectibles;
using UnityEngine;

namespace _Skycall.Scripts.Enemies.EnemyShip
{
    public class EnemyScore : ScorerBase
    {
        [SerializeField] int scoreValue;

        public int ScoreValue
        {
            get => scoreValue;
            set => scoreValue = value;
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Tags.T_shoot))
            {
                ScorerBase.OnScoreHandler(ScoreValue);
            }
        }
    }
}