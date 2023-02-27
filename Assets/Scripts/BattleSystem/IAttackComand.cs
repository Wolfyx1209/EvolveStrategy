using TileSystem;
namespace BattleSystem 
{
    public interface IAttackComand : IComand
    {
        public PlayersList GetAttackingPlayer();
    }
}