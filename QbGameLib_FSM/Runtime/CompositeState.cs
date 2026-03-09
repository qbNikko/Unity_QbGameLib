namespace QbGameLib.FSM
{
    public class CompositeState :  AbstractCompositeState
    {
        private string _name;
        public CompositeState(int countSubstate, string name) : base(countSubstate)
        {
            this._name = name;
        }

        public override string Name()
        {
            return _name;
        }
    }
}