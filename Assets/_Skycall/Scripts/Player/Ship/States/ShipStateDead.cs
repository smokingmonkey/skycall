using _Skycall.Scripts.Util;
using Zenject;

namespace _Skycall.Scripts.Player.Ship.States
{
    public class ShipStateDead : ShipState
    {
        readonly Ship _ship;


        public ShipStateDead(Ship ship)
        {
            _ship = ship;
        }

        public override void Start()
        {
            _ship.MeshRenderer.enabled = false;

            _ship.ParticleEmitter.gameObject.SetActive(false);


            _ship.OnShipCrashed();
        }

        public override void Dispose()
        {
            _ship.MeshRenderer.enabled = true;

            _ship.ParticleEmitter.gameObject.SetActive(true);
        }

        public override void Update()
        {
        }


        public class Factory : PlaceholderFactory<ShipStateDead>
        {
        }
    }
}