using TileSystem;

public class NoneAcktor : GameAcktor
{
    public NoneAcktor(TerrainTilemap terrainTilemap): base(PlayersList.None, terrainTilemap) 
    {    }

    public override void OfferToBuildNest(Region region)
    {   }
}

