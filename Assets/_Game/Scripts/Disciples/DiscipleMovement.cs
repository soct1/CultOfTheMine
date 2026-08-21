using UnityEngine;
using CultOfTheMine.Mining;

namespace CultOfTheMine.Disciples
{
    public class DiscipleMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 2f;

        [Header("Points")]
        [SerializeField] private Transform pickaxeDamagePoint;

        [Header("Mining")]
        [SerializeField] private Pickaxe pickaxe;

        [Header("Visuals")]
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Transform pickaxeVisual;

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
            if (target == null ||
                pickaxeDamagePoint == null ||
                pickaxe == null)
            {
                return false;
            }

            Collider2D mineCollider =
                target.GetComponent<Collider2D>();

            if (mineCollider == null)
                return false;

            Vector2 hitPosition =
                pickaxeDamagePoint.position;

            Vector2 closestPoint =
                mineCollider.ClosestPoint(hitPosition);

            float distanceSqr =
                (closestPoint - hitPosition).sqrMagnitude;

            float radius =
                pickaxe.MiningRadius;

            return distanceSqr <= radius * radius;
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

            Vector2 direction =
                target.transform.position - transform.position;

            if (direction.sqrMagnitude <= 0.0001f)
                return;

            direction.Normalize();

            transform.position +=
                (Vector3)direction *
                (moveSpeed * Time.deltaTime);

            UpdateFacing(direction);
        }

        private void UpdateFacing(Vector2 direction)
        {
            if (Mathf.Abs(direction.x) < 0.01f)
                return;

            bool facingLeft = direction.x < 0f;

            if (spriteRenderer != null)
                spriteRenderer.flipX = facingLeft;

            if (pickaxeVisual != null)
            {
                Vector3 scale =
                    pickaxeVisual.localScale;

                scale.x =
                    Mathf.Abs(scale.x) *
                    (facingLeft ? -1f : 1f);

                pickaxeVisual.localScale = scale;
            }

            MirrorPoint(pickaxeDamagePoint, facingLeft);
        }

        private void MirrorPoint(
            Transform point,
            bool facingLeft)
        {
            if (point == null)
                return;

            Vector3 position =
                point.localPosition;

            position.x =
                Mathf.Abs(position.x) *
                (facingLeft ? -1f : 1f);

            point.localPosition = position;
        }

        private void OnDrawGizmosSelected()
        {
            if (pickaxeDamagePoint == null)
                return;

            float radius =
                pickaxe != null
                    ? pickaxe.MiningRadius
                    : 0f;

            Gizmos.DrawWireSphere(
                pickaxeDamagePoint.position,
                radius
            );

            Gizmos.DrawWireSphere(
                pickaxeDamagePoint.position,
                0.05f
            );
        }
    }
}