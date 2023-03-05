using TileSystem;

namespace EventBusSystem
{
    public interface IRegionControleOnePlayerHandler : IGlobalSubscriber
    {
        public void RegionControlOnePlayer(Region region, PlayersList owner);
    }
}
