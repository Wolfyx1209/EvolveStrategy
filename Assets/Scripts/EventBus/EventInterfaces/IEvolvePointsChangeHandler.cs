namespace EventBusSystem
{
    public interface IEvolvePointsChangeHandler : IGlobalSubscriber
    {
        public void EvolvePointsChanges(int value);
    }
}


