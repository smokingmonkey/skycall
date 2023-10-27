using System;
using UnityEngine;

namespace _Skycall.Scripts.Player.Ship.States
{
    public abstract class ShipState : IDisposable
    {
        public abstract void Update();

        public virtual void Start()
        {
        }

        public virtual void Dispose()
        {
        }

        public virtual void OnTriggerEnter(Collider other)
        {
        }
    }
}
