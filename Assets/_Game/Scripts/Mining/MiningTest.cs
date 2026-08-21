using UnityEngine;

namespace CultOfTheMine.Mining
{
    public class MiningTest : MonoBehaviour
    {
        [SerializeField] private Pickaxe pickaxe;
        [SerializeField] private MineNode mine;

        [ContextMenu("Test Mining Hit")]
        private void TestMiningHit()
        {
            if (pickaxe == null)
            {
                Debug.LogError("MiningTest has no Pickaxe assigned.", this);
                return;
            }

            if (mine == null)
            {
                Debug.LogError("MiningTest has no MineNode assigned.", this);
                return;
            }

            int damage = pickaxe.CalculateDamage(mine);

            Debug.Log(
                $"Pickaxe Power: {pickaxe.Power}, " +
                $"Hardness: {mine.Config.Hardness}, " +
                $"Damage: {damage}"
            );

            pickaxe.Mine(mine);
        }
    }
}