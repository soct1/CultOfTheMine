using UnityEngine;

namespace CultOfTheMine.Mining
{
    public class Pickaxe : MonoBehaviour
    {
        [SerializeField] private PickaxeConfig config;

        public PickaxeConfig Config => config;
        public int Power => config != null ? config.Power : 0;

        public int CalculateDamage(MineNode mine)
        {
            if (mine == null || config == null)
                return 0;

            float rawDamage = config.Power / mine.Config.Hardness;

            return Mathf.Max(1, Mathf.FloorToInt(rawDamage));
        }

        public void Mine(MineNode mine)
        {
            if (mine == null || mine.IsBroken)
                return;

            int damage = CalculateDamage(mine);

            mine.TakeDamage(damage);
        }
    }
}