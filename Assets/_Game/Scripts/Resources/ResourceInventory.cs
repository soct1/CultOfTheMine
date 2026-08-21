using System;
using System.Collections.Generic;
using UnityEngine;

namespace CultOfTheMine.Resources
{
    public class ResourceInventory : MonoBehaviour
    {
        private readonly Dictionary<ResourceConfig, int> amounts = new();

        public event Action<ResourceConfig, int> ResourceChanged;

        public int GetAmount(ResourceConfig resource)
        {
            if (resource == null)
                return 0;

            return amounts.TryGetValue(resource, out int amount)
                ? amount
                : 0;
        }

        public void Add(ResourceConfig resource, int amount)
        {
            if (resource == null)
            {
                Debug.LogError("Cannot add a null resource.");
                return;
            }

            if (amount <= 0)
                return;

            if (!amounts.ContainsKey(resource))
                amounts[resource] = 0;

            amounts[resource] += amount;

            int total = amounts[resource];

            Debug.Log(
                $"Resource added: {resource.DisplayName} +{amount}. Total: {total}"
            );

            ResourceChanged?.Invoke(resource, total);
        }
    }
}