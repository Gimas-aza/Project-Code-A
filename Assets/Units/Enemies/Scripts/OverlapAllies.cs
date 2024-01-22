using Assets.Units.Base;
using Assets.Units.FSM;

namespace Assets.Units.Enemies
{
    public class OverlapAllies : OverlapBase 
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
                if (OverlapResults[i].TryGetComponent(out FsmEnemy allie) == false)
                {
                    continue;
                }
                
                if (CheckObstacle(OverlapResults[i]))
                {
                    continue;
                }
                
                allie.SetStateAttack();
            }
        }
    } 
}