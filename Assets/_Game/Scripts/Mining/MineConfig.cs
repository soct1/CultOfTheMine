using UnityEngine;
using CultOfTheMine.Resources;

namespace CultOfTheMine.Mining
{
    [CreateAssetMenu(
        fileName = "MineConfig",
        menuName = "Cult of the Mine/Mining/Mine Config")]
    public class MineConfig : ScriptableObject
    {
        [Header("Identity")]
        [SerializeField] private string mineName;

        [Header("Mining")]
        [SerializeField] private int maxHP;
        [SerializeField] private float hardness = 1f;

        [Header("Resource")]
        [SerializeField] private ResourceConfig resource;

        public string MineName => mineName;
        public int MaxHP => maxHP;
        public float Hardness => hardness;
        public ResourceConfig Resource => resource;
    }
}