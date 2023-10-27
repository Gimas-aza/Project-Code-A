using UnityEngine;

namespace Assets.Units.Player.Base
{
    public abstract class AttackBehaviour : MonoBehaviour
    {
        public bool IsAccessToAttack;

        public abstract void PerformAttack();
    }
}