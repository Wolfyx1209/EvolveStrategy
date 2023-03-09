using TileSystem;

namespace EventBusSystem
{
    public interface ICellChangeOwnerHandler : IGlobalSubscriber
    {
        public void ChangeOwner(GameAcktor previousOwner, GameAcktor newOwner, TerrainCell cell);
    }
}

