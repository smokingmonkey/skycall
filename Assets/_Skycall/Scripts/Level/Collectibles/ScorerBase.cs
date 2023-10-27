using System;
using UnityEngine;

namespace _Skycall.Scripts.Level.Collectibles
{
    public abstract class ScorerBase : MonoBehaviour
    {
        public static event Action<int> OnScore;

        public static void OnScoreHandler(int obj)
        {
            OnScore?.Invoke(obj);
        }
    }
}
