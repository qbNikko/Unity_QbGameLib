using System;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.Pool;

namespace QbGameLib.FSM
{
    public class FsmArgs : IDisposable
    {
        internal readonly Dictionary<string, object> _args;
        public FsmArgs()
        {
            DictionaryPool<string, object>.Get(out _args);
        }

        public FsmArgs Add(string key, object value)
        {
            _args.Add(key, value);
            return this;
        }

        public void Dispose()
        {
            DictionaryPool<string, object>.Release(_args);
        }
    }
    public class StateMachine : IDisposable
    {
        private static IState emptyState =  new EmptyState();
        private readonly Dictionary<string, IState> _statesDictionary;
        private readonly Dictionary<Type, IState> _statesTypeDictionary;
        private readonly Dictionary<IFsmTrigger, FsmTriggerAction> _triggers;
        private Dictionary<string, object> _flags;
        private IState _currentState;
        public UnityEvent<IState> OnEnterState;
        
        public StateMachine()
        {
            OnEnterState = new UnityEvent<IState>();
            DictionaryPool<string, IState>.Get(out _statesDictionary);
            DictionaryPool<Type, IState>.Get(out _statesTypeDictionary);
            DictionaryPool<IFsmTrigger, FsmTriggerAction>.Get(out _triggers);
            _currentState = emptyState;
            _flags = new Dictionary<string, object>();
        }

        public Dictionary<string, object> Flags => _flags;

        public StateMachine RegisterState(IState state)
        {
            _statesDictionary.Add(state.Name(), state);
            _statesTypeDictionary.Add(state.GetType(), state);
            state.StateMachine = this;
            return this;
        }
        
        public StateMachine RegisterTrigger(IFsmTrigger fsmTrigger, Type state)
        {
            if(_triggers.ContainsKey(fsmTrigger)) throw new AggregateException("Trigger already exists");
            FsmTriggerAction action = () => SwitchState(state);
            fsmTrigger.AddListener(action);
            _triggers.Add(fsmTrigger, action);
            return this;
        }
        public StateMachine UnregisterTrigger(IFsmTrigger fsmTrigger)
        {
            if (_triggers.TryGetValue(fsmTrigger, out FsmTriggerAction action))
            {
                fsmTrigger.RemoveListener(action);
                _triggers.Remove(fsmTrigger);
            }
            return this;
        }
        
        public void SwitchState<T>(FsmArgs parameters = null) where T : class, IState
        {
            Type type = typeof(T);
            if (type!=null && _statesTypeDictionary.TryGetValue(type, out var newState))
            {
                _currentState.Exit();
                _currentState = newState;
                _currentState.Enter(parameters._args);
                OnEnterState.Invoke(_currentState);
            }
            parameters?.Dispose();
        }
        public void SwitchState(Type state, FsmArgs parameters = null)
        {
            if (state!=null && _statesTypeDictionary.TryGetValue(state, out var newState))
            {
                _currentState.Exit();
                _currentState = newState;
                _currentState.Enter(parameters._args);
                OnEnterState.Invoke(_currentState);
            }
            parameters?.Dispose();
        }
        
        public void SwitchState(string state, FsmArgs parameters = null)
        {
            if (state!=null && _statesDictionary.TryGetValue(state, out var newState))
            {
                _currentState.Exit();
                _currentState = newState;
                _currentState.Enter(parameters._args);
                OnEnterState.Invoke(_currentState);
            }
            parameters?.Dispose();
        }
        
        public Type CurrentStateType => _currentState.GetType();
        public IState CurrentState => _currentState;
        public bool IsState<T>() => _currentState is T;
        public string CurrentStateName => _currentState.Name();

        public void HandleEvent(string eventName)
        {
            _currentState.HandleEvent(eventName);
        }

        public void Update()
        {
            _currentState.Update();
        }

        public void FixedUpdate()
        {
            _currentState.FixedUpdate();
        }

        public void Dispose()
        {
            foreach (var fsmTriggerAction in _triggers)
            {
                fsmTriggerAction.Key.RemoveListener(fsmTriggerAction.Value);
            }
            foreach (IState state in _statesDictionary.Values)
            {
                state.Dispose();
            }
            DictionaryPool<string, IState>.Release(_statesDictionary);
            DictionaryPool<Type, IState>.Release(_statesTypeDictionary);
            DictionaryPool<IFsmTrigger, FsmTriggerAction>.Release(_triggers);
        }
    }
}