using TileSystem;

public class NoneAcktor : GameAcktor
{
    protected new void Awake()
    {
        acktorName = PlayersList.None;
        base.Awake();
    }
    public override void OfferToBuildNest(Region region)
    {   }
}

