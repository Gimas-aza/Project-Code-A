using System;
using System.Collections.Generic;

namespace Assets.FSM
{
    public class Fsm
    {
        private FsmState _currentState { get; set; }
        private Dictionary<Type, FsmState> _states = new();

        public void AddState(FsmState state)
        {
            _states.Add(state.GetType(), state);
        }

        public void SetState<T>() where T : FsmState
        {
            var type = typeof(T);

            if (_currentState != null && _currentState.GetType() == type)
                return;

            if (_states.TryGetValue(type, out var newState))
            {
                _currentState?.Exit();

                _currentState = newState;

                _currentState.Enter();
            }
        }

        public void Update()
        {
            _currentState?.Update();
        }
    }
}