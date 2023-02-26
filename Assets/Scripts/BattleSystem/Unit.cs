using Unity.VisualScripting;

namespace BattleSystem 
{ 
    public class Unit
    {
        public Unit(PlayersList owner) 
        {
            this.owner = owner;
            damage = 10;
            defense = 10;
            speed = 1;
        }
        public PlayersList owner;
        public int damage;
        public int defense;
        public float speed; 
    }
}

