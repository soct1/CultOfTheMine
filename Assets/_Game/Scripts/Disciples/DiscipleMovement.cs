using UnityEngine;
using CultOfTheMine.Mining;

namespace CultOfTheMine.Disciples
{
    public class DiscipleMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 2f;
        [SerializeField] private float miningRange = 0.8f;

        private MineNode target;

        public bool HasTarget => target != null;

        public void SetTarget(MineNode newTarget)
        {
            target = newTarget;
        }

        public void ClearTarget()
        {
            target = null;
        }

        public bool IsInMiningRange()
        {
            if (target == null)
                return false;

            float distanceSqr =
                (target.transform.position - transform.position).sqrMagnitude;

            return distanceSqr <= miningRange * miningRange;
        }

        private void Update()
        {
            if (target == null)
                return;

            if (target.IsBroken)
            {
                ClearTarget();
                return;
            }

            if (IsInMiningRange())
                return;

            Vector3 direction =
                (target.transform.position - transform.position).normalized;

            transform.position +=
                direction * (moveSpeed * Time.deltaTime);
        }
    }
}