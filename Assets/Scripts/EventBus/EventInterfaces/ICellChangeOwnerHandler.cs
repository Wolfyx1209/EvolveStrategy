using TileSystem;

namespace EventBusSystem
{
    public interface ICellChangeOwnerHandler : IGlobalSubscriber
    {
        public void ChangeOwner(PlayersList previousOwner, PlayersList newOwner, TerrainCell cell);
    }
}

