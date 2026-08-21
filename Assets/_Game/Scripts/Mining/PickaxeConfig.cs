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
        [SerializeField] private float miningRadius = 0.15f;
        [SerializeField] private float impactDistance = 0.75f;
        [SerializeField] private float contactTolerance = 0.05f;
        public string PickaxeName => pickaxeName;
        public int Power => power;
        public float MiningRadius => miningRadius;
        public float ImpactDistance => impactDistance;
        public float ContactTolerance => contactTolerance;
    }
}