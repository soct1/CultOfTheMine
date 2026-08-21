using UnityEngine;
using CultOfTheMine.Mining;

namespace CultOfTheMine.Disciples
{
    public class TargetSelectionTest : MonoBehaviour
    {
        [SerializeField] private MineTargetSelector selector;
        [SerializeField] private DiscipleMovement movement;
        [SerializeField] private DiscipleMining mining;

        [ContextMenu("Find And Mine Nearest Mine")]
        private void FindAndMineNearestMine()
        {
            if (selector == null)
            {
                Debug.LogError(
                    "TargetSelectionTest has no selector assigned.",
                    this
                );
                return;
            }

            if (movement == null)
            {
                Debug.LogError(
                    "TargetSelectionTest has no movement assigned.",
                    this
                );
                return;
            }

            if (mining == null)
            {
                Debug.LogError(
                    "TargetSelectionTest has no mining assigned.",
                    this
                );
                return;
            }

            MineNode target = selector.FindNearestMine();

            if (target == null)
            {
                Debug.Log("No available mine found.");
                return;
            }

            Debug.Log($"Selected target: {target.Config.MineName}");

            movement.SetTarget(target);
            mining.SetTarget(target);
        }
    }
}