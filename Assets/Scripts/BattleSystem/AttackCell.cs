using System.Collections;
using TileSystem;
namespace BattleSystem
{
    public class AttackCell : IAttackComand
    {
        #region events
        public delegate void AttackEnd(TerrainCell to, Unit unit, int unitCount);
        public event AttackEnd OnAttackEnd;

        public delegate void ComandEnd(IComand comand);
        public event ComandEnd OnComandEnd;
        #endregion

        private float progress;
        private Timer _timer = new();
        private TerrainCell _to;
        private Unit _unit;
        private int _unitCount;

        public AttackCell(TerrainCell to, Unit unit, int unitCount, float timeInSeconds)
        {
            _to = to;
            _unit = unit;
            _unitCount = unitCount;
            _timer.StartTimer(timeInSeconds);
            _timer.OnTimeOver += CommandSucsess;
            Coroutines.StartRoutine(UpdateProgress());
        }

        private IEnumerator UpdateProgress()
        {
            while (progress < 1)
            {
                progress = _timer.progress;
                yield return null;
            }
            progress = 1;
        }
        private void CommandSucsess()
        {
            OnAttackEnd?.Invoke(_to, _unit, _unitCount);
            OnComandEnd?.Invoke(this);
            Coroutines.StopRoutine(UpdateProgress());
        }

        public float GetProgress()
        {
            return progress;
        }
    }
}


