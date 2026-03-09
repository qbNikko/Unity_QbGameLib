using System;
using System.Collections.Generic;

namespace QbGameLib.FSM
{
    public interface IState : IDisposable
    {
        public string Name();
        public StateMachine StateMachine { get; internal set; }

        public void Enter(IReadOnlyDictionary<string,object> parameters = null) {}
        
        public void Exit() {}
        
        public void Update() { }
        
        public void FixedUpdate() {}
        void HandleEvent(string eventName) {}
    }
    
    public abstract class State : IState
    {
        private string _name;
        protected StateMachine _stateMachine;
        
        public State()
        {
            _name = GetType().Name;
        }
        public State(string name)
        {
            _name = name;
        }

        public string Name()
        {
            return _name;
        }
        
        StateMachine IState.StateMachine
        {
            get => _stateMachine;
            set => _stateMachine = value;
        }

        public virtual void Enter(IReadOnlyDictionary<string, object> parameters = null)
        {
        }

        public virtual void Exit()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void FixedUpdate()
        {
        }

        public virtual void HandleEvent(string eventName)
        {
        }
        
        public virtual void Dispose()
        {
        }
    }
}