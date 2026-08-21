using UnityEngine;
using CultOfTheMine.Mining;

namespace CultOfTheMine.Disciples
{
    public class DiscipleMining : MonoBehaviour
    {
        [SerializeField] private DiscipleMovement movement;
        [SerializeField] private PickaxeConfig pickaxe;
        [SerializeField] private float miningInterval = 1f;

        private MineNode target;
        private float miningTimer;

        public void SetTarget(MineNode newTarget)
        {
            target = newTarget;
            miningTimer = 0f;
        }

        public void ClearTarget()
        {
            target = null;
            miningTimer = 0f;
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

            if (movement == null || !movement.IsInMiningRange())
                return;

            miningTimer -= Time.deltaTime;

            if (miningTimer > 0f)
                return;

            Mine();

            miningTimer = miningInterval;
        }

        private void Mine()
        {
            if (pickaxe == null)
            {
                Debug.LogError(
                    $"DiscipleMining on '{name}' has no PickaxeConfig assigned.",
                    this
                );
                return;
            }

            int damage = Mathf.Max(
                1,
                Mathf.FloorToInt(
                    pickaxe.Power / target.Config.Hardness
                )
            );

            target.TakeDamage(damage);
        }
    }
}