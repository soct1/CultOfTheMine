using UnityEngine;

namespace CultOfTheMine.Mining
{
    [CreateAssetMenu(
        fileName = "PickaxeConfig",
        menuName = "Cult of the Mine/Mining/Pickaxe Config")]
    public class PickaxeConfig : ScriptableObject
    {
        [Header("Identity")]
        [SerializeField] private string pickaxeName;

        [Header("Mining")]
        [SerializeField] private int power = 10;

        public string PickaxeName => pickaxeName;
        public int Power => power;
    }
}