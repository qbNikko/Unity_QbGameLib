using System.Collections.Generic;

namespace QbGameLib.FSM
{
    public interface ISubState
    {
        public bool Enabled { get; set; }
        public IState State { get; internal set; }
    }
    public interface IEnterSubState :  ISubState
    {
        public void Enter(IReadOnlyDictionary<string,object> parameters = null);
    }
    public interface IExitSubState :  ISubState
    {
        public void Exit();
    }
    public interface IUpdateSubState :  ISubState
    {
        public void Update();
    }
    public interface IFixUpdateSubState :  ISubState
    {
        public void FixedUpdate();
    }
    public interface IHandleEventSubState :  ISubState
    {
        public void HandleEvent(string eventName);
    }
}