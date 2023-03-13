namespace EventBusSystem
{
    public interface IEvolvePointsChangeHandler : IGlobalSubscriber
    {
        public bool EvolvePointsChanges(int value);
    }
}


