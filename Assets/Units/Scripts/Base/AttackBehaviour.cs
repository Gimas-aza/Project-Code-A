using UnityEngine;

namespace Assets.Units.Base
{
    public abstract class AttackBehaviour : MonoBehaviour
    {
        public bool IsAccessToAttack;

        public abstract void PerformAttack(float force = 0);
    }
}