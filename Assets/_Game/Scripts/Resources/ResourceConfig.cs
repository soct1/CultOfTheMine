using UnityEngine;

namespace CultOfTheMine.Resources
{
    [CreateAssetMenu(
        fileName = "ResourceConfig",
        menuName = "Cult of the Mine/Resources/Resource Config")]
    public class ResourceConfig : ScriptableObject
    {
        [SerializeField] private string resourceId;
        [SerializeField] private string displayName;

        public string ResourceId => resourceId;
        public string DisplayName => displayName;
    }
}