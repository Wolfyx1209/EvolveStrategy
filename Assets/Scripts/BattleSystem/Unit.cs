using System;

namespace BattleSystem
{
    public class Unit
    {
        public Unit(PlayersList owner)
        {
            this.owner = owner;
            _attack = 10;
            _defense = 10;
            _moveSpeed = 1;
            _spawnSpeed = 1;
        }

        private int _attack;
        private int _defense;
        private float _moveSpeed;
        private float _spawnSpeed;
        private float _swimSpeedTime;
        private float _climbSpeed;
        private float _coldResistance;
        private float _heatResistance;
        private float _poisonResistance;

        public PlayersList owner;

        public int attack
        {
            get => _attack > 0 ? _attack : 1;
            set => _attack = value;
        }
        public int defense
        {
            get => _defense > 0 ? _defense : 1;
            set => _defense = value;
        }

        public float moveSpeed
        {
            get => _moveSpeed < 0.1f ? 0.1f : _moveSpeed;
            set => _moveSpeed = value;
        }
        public float spawnSpeed
        {
            get => _spawnSpeed < 0.1f ? 0.1f : _spawnSpeed;
            set => _spawnSpeed = value;
        }

        public float swimSpeedTime
        {
            get => _swimSpeedTime < 0 ? 0 : _swimSpeedTime;
            set => _swimSpeedTime = value;
        }
        public float climbSpeed
        {
            get => _climbSpeed < 0 ? 0 : _climbSpeed;
            set => _climbSpeed = value;
        }

        public float coldResistance
        {
            get => Math.Clamp(_coldResistance, 0, 1);
            set => _coldResistance = value;
        }
        public float heatResistance
        {
            get => Math.Clamp(_heatResistance, 0, 1);
            set => _heatResistance = value;
        }
        public float poisonResistance
        {
            get => Math.Clamp(_poisonResistance, 0, 1);
            set => _poisonResistance = value;
        }
    }
}

