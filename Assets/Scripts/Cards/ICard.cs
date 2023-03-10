namespace CardSystem
{
    public interface ICard
    {
        bool IsEquiped { get; }

        CardType cardType { get; }
    }

}

