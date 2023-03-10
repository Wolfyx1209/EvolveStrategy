namespace CardSystem
{
    public interface ICardPlaceholder
    {
        CardType slotType { get; }
        bool isEmpty { get; }

        ICard card { get; }
        public bool TryPlace—ard(ICard card);
    }
}
