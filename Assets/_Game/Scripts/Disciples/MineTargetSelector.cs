using UnityEngine;
using CultOfTheMine.Mining;

namespace CultOfTheMine.Disciples
{
    public class MineTargetSelector : MonoBehaviour
    {
        [SerializeField] private MineSpawner mineSpawner;

        public MineNode FindNearestMine()
        {
            if (mineSpawner == null)
            {
                Debug.LogError(
                    "MineTargetSelector has no MineSpawner assigned.",
                    this
                );

                return null;
            }

            MineNode nearestMine = null;
            float nearestDistanceSqr = float.MaxValue;

            foreach (MineNode mine in mineSpawner.ActiveMines)
            {
                if (mine == null || mine.IsBroken)
                    continue;

                float distanceSqr =
                    (mine.transform.position - transform.position)
                    .sqrMagnitude;

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