using UnityEngine;
using CultOfTheMine.Mining;

namespace CultOfTheMine.Disciples
{
    public class MineTargetSelector : MonoBehaviour
    {
        [SerializeField] private float searchRadius = 100f;

        public MineNode FindNearestMine()
        {
            MineNode[] mines = FindObjectsByType<MineNode>(
                FindObjectsInactive.Exclude
            );

            MineNode nearestMine = null;
            float nearestDistanceSqr = float.MaxValue;

            foreach (MineNode mine in mines)
            {
                if (mine == null || mine.IsBroken)
                    continue;

                float distanceSqr =
                    (mine.transform.position - transform.position).sqrMagnitude;

                if (distanceSqr > searchRadius * searchRadius)
                    continue;

                if (distanceSqr < nearestDistanceSqr)
                {
                    nearestDistanceSqr = distanceSqr;
                    nearestMine = mine;
                }
            }

            return nearestMine;
        }
    }
}