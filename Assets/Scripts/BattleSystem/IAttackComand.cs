using TileSystem;
namespace BattleSystem 
{
    public interface IAttackComand : IComand
    {
        public GameAcktor GetAttackingPlayer();
    }
}