using _Skycall.Scripts.Models;
using _Skycall.Scripts.Player.Ship.States;
using ModestTree;

namespace _Skycall.Scripts.Player.Ship
{
    public class ShipStateFactory
    {
        readonly ShipStateWaitingToStart.Factory _waitingFactory;
        readonly ShipStateMoving.Factory _movingFactory;
        readonly ShipStateDead.Factory _deadFactory;

        public ShipStateFactory(
            ShipStateDead.Factory deadFactory,
            ShipStateMoving.Factory movingFactory,
            ShipStateWaitingToStart.Factory waitingFactory)
        {
            _waitingFactory = waitingFactory;
            _movingFactory = movingFactory;
            _deadFactory = deadFactory;
        }

        public ShipState CreateState(ShipStates state)
        {
            switch (state)
            {
                case ShipStates.Dead:
                {
                    return _deadFactory.Create();
                }
                case ShipStates.WaitingToStart:
                {
                    return _waitingFactory.Create();
                }
                case ShipStates.Moving:
                {
                    return _movingFactory.Create();
                }
            }

            throw Assert.CreateException();
        }
    }
}