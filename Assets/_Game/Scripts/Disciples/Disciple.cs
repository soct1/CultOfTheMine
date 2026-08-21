using UnityEngine;
using CultOfTheMine.Mining;

namespace CultOfTheMine.Disciples
{
    public class Disciple : MonoBehaviour
    {
        [SerializeField] private DiscipleConfig config;

        public DiscipleConfig Config => config;
        public string DiscipleName => config != null ? config.DiscipleName : string.Empty;
        public PickaxeConfig Pickaxe => config != null ? config.Pickaxe : null;
    }
}