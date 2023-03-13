namespace CardSystem
{
    public interface ICard
    {
        bool IsEquiped { get; }

        CardData cardData { get; }
    }

}

