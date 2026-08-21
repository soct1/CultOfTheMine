using UnityEngine;
using CultOfTheMine.Resources;

namespace CultOfTheMine.Mining
{
    public class MineNode : MonoBehaviour
    {
        [SerializeField] private MineConfig config;
        [SerializeField] private DamageNumber damageNumberPrefab;
        [SerializeField] private ResourceInventory resourceInventory;

        private int currentHP;

        public MineConfig Config => config;
        public int CurrentHP => currentHP;
        public bool IsBroken => currentHP <= 0;

        private void Awake()
        {
            Initialize();
        }

        public void Initialize()
        {
            if (config == null)
            {
                Debug.LogError($"MineNode on '{name}' has no MineConfig assigned.", this);
                currentHP = 0;
                return;
            }

            currentHP = config.MaxHP;
        }

        public void TakeDamage(int damage)
        {
            if (IsBroken)
                return;

            if (damage <= 0)
                return;

            currentHP -= damage;

            if (currentHP < 0)
                currentHP = 0;

            ShowDamageNumber(damage);

            Debug.Log(
                $"{config.MineName} took {damage} damage. " +
                $"HP: {currentHP}/{config.MaxHP}"
            );

            if (IsBroken)
            {
                Break();
            }
        }

        private void Break()
        {
            Debug.Log($"{config.MineName} is broken.");

            if (resourceInventory == null)
            {
                Debug.LogError(
                    $"MineNode on '{name}' has no ResourceInventory assigned.",
                    this
                );

                return;
            }

            if (config.Resource == null)
            {
                Debug.LogError(
                    $"MineConfig '{config.name}' has no Resource assigned.",
                    config
                );

                return;
            }

            resourceInventory.Add(config.Resource, 1);
        }

        private void ShowDamageNumber(int damage)
        {
            if (damageNumberPrefab == null)
                return;

            DamageNumber damageNumber = Instantiate(
                damageNumberPrefab,
                transform.position + Vector3.up * 0.5f,
                Quaternion.identity
            );

            damageNumber.Initialize(damage);
        }
    }
}