using TMPro;
using UnityEngine;

namespace CultOfTheMine.Resources
{
    public class ResourceHUD : MonoBehaviour
    {
        [Header("Inventory")]
        [SerializeField] private ResourceInventory inventory;

        [Header("Resources")]
        [SerializeField] private ResourceConfig stoneResource;
        [SerializeField] private ResourceConfig coalResource;
        [SerializeField] private ResourceConfig copperResource;

        [Header("UI")]
        [SerializeField] private TMP_Text stoneText;
        [SerializeField] private TMP_Text coalText;
        [SerializeField] private TMP_Text copperText;

        private void OnEnable()
        {
            if (inventory == null)
                return;

            inventory.ResourceChanged += OnResourceChanged;

            RefreshAll();
        }

        private void OnDisable()
        {
            if (inventory == null)
                return;

            inventory.ResourceChanged -= OnResourceChanged;
        }

        private void OnResourceChanged(ResourceConfig resource, int amount)
        {
            if (resource == null)
                return;

            if (resource == stoneResource)
            {
                UpdateText(stoneText, "Stone", amount);
            }
            else if (resource == coalResource)
            {
                UpdateText(coalText, "Coal", amount);
            }
            else if (resource == copperResource)
            {
                UpdateText(copperText, "Copper", amount);
            }
        }

        private void RefreshAll()
        {
            UpdateText(
                stoneText,
                "Stone",
                inventory.GetAmount(stoneResource)
            );

            UpdateText(
                coalText,
                "Coal",
                inventory.GetAmount(coalResource)
            );

            UpdateText(
                copperText,
                "Copper",
                inventory.GetAmount(copperResource)
            );
        }

        private void UpdateText(TMP_Text target, string label, int amount)
        {
            if (target == null)
                return;

            target.text = $"{label}: {amount}";
        }
    }
}