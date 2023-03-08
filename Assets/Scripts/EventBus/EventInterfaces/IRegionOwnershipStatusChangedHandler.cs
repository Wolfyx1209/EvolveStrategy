using TileSystem;

namespace EventBusSystem
{
    public interface IRegionOwnershipStatusChangedHandler : IGlobalSubscriber
    {
        public void RegionControledBySinglePlayer(Region region, PlayersList owner);
        public void RegionNoLongerControlledBySinglePlayer(Region region);
    }
}
