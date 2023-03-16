namespace EventBusSystem
{
    public interface IEvolvePointsChangeHandler : IGlobalSubscriber
    {
        public void EvolvePointsChanges(PlayersList acktor, int value);
    }
}


