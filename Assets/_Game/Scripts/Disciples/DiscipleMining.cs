using UnityEngine;
using CultOfTheMine.Mining;

namespace CultOfTheMine.Disciples
{
    public class DiscipleMining : MonoBehaviour
    {
        [SerializeField] private DiscipleMovement movement;
        [SerializeField] private Pickaxe pickaxe;
        [SerializeField] private MineSpawner mineSpawner;

        [SerializeField] private Transform pickaxeDamagePoint;
        [SerializeField] private float miningInterval = 1f;

        private MineNode target;
        private float nextMiningTime;

        public void SetTarget(MineNode newTarget)
        {
            target = newTarget;
        }

        public void ClearTarget()
        {
            target = null;
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

            if (movement == null ||
                !movement.IsInMiningRange())
            {
                return;
            }

            if (Time.time < nextMiningTime)
                return;

            Mine();

            nextMiningTime =
                Time.time + miningInterval;
        }

        private void Mine()
        {
            if (pickaxe == null ||
                pickaxeDamagePoint == null ||
                mineSpawner == null)
            {
                return;
            }

            Vector2 hitPosition =
                pickaxeDamagePoint.position;

            float radius =
                pickaxe.MiningRadius;

            float radiusSqr =
                radius * radius;

            foreach (MineNode mine in mineSpawner.ActiveMines)
            {
                if (mine == null ||
                    mine.IsBroken)
                {
                    continue;
                }

                Collider2D mineCollider =
                    mine.GetComponent<Collider2D>();

                if (mineCollider == null)
                    continue;

                Vector2 closestPoint =
                    mineCollider.ClosestPoint(hitPosition);

                float distanceSqr =
                    (closestPoint - hitPosition).sqrMagnitude;

                if (distanceSqr > radiusSqr)
                    continue;

                int damage =
                    pickaxe.CalculateDamage(mine);

                mine.TakeDamage(damage);
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (pickaxeDamagePoint == null ||
                pickaxe == null)
            {
                return;
            }

            Gizmos.DrawWireSphere(
                pickaxeDamagePoint.position,
                pickaxe.MiningRadius
            );

            Gizmos.DrawWireSphere(
                pickaxeDamagePoint.position,
                0.05f
            );
        }
    }
}