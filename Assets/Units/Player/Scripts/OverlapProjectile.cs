using Assets.Units.Base;
using Assets.Units.FSM;

namespace Assets.Units.Player
{
    public class OverlapProjectile : OverlapBase
    {
        public override void StartAction()
        {
            if (TryFind())
            {
                TryAction();
            }
        }

        protected override void TryAction()
        {
            for (int i = 0; i < OverlapResultsCount; i++)
            {
                if (OverlapResults[i].TryGetComponent(out FsmEnemy enemy) == false)
                {
                    continue;
                }
                
                if (CheckObstacle(OverlapResults[i]))
                {
                    continue;
                }
                
                enemy.ComeUpWith(OverlapStartPoint.position);
            }
        }
    }
}
