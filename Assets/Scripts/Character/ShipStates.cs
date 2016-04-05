using System.Collections.Generic;

namespace Assets.Scripts.Character
{
    public class ShipState : Components.StatesBase<ShipState.States>
    {
        public enum States
        {
            Idle,
            Forward,
            Backward,
            Turn,
            Die
        }

        public override States currentState { get; protected set; }
        public List<States> moveStates = new List<States>{ States.Forward, States.Backward, States.Turn };
    }
}
