using System;

namespace _Skycall.Scripts.Level
{
    public interface ICollectible
    {
        public static event Action<float> OnCollected;

        void Collect();
    }
}