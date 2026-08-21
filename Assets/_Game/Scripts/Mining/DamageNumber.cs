using TMPro;
using UnityEngine;

namespace CultOfTheMine.Mining
{
    public class DamageNumber : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private float lifetime = 0.8f;
        [SerializeField] private float moveSpeed = 1.5f;

        private float timer;

        public void Initialize(int damage)
        {
            if (text == null)
            {
                Debug.LogError($"DamageNumber on '{name}' has no TMP_Text assigned.", this);
                return;
            }

            text.text = $"-{damage}";
            timer = lifetime;
        }

        private void Update()
        {
            transform.position += Vector3.up * (moveSpeed * Time.deltaTime);

            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }
}