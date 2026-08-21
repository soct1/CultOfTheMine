using UnityEngine;
using CultOfTheMine.Mining;

namespace CultOfTheMine.Disciples
{
    [CreateAssetMenu(
        fileName = "DiscipleConfig",
        menuName = "Cult of the Mine/Disciples/Disciple Config")]
    public class DiscipleConfig : ScriptableObject
    {
        [Header("Identity")]
        [SerializeField] private string discipleName;

        [Header("Mining")]
        [SerializeField] private PickaxeConfig pickaxe;

        public string DiscipleName => discipleName;
        public PickaxeConfig Pickaxe => pickaxe;
    }
}