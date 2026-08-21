using System.Collections.Generic;
using UnityEngine;

namespace CultOfTheMine.Mining
{
    public class MineSpawner : MonoBehaviour
    {
        [Header("Mine Prefabs")]
        [SerializeField] private MineNode[] minePrefabs;

        [Header("Spawn Area")]
        [SerializeField] private Transform spawnArea;
        [SerializeField] private Transform mineParent;

        [Header("Wave")]
        [SerializeField] private int mineCount = 10;

        [Header("Spacing")]
        [SerializeField] private float minimumMineDistance = 1.2f;

        [SerializeField] private int maxSpawnAttempts = 50;
        private readonly List<MineNode> activeMines = new();

        public IReadOnlyList<MineNode> ActiveMines => activeMines;

        private void Start()
        {
            SpawnWave();
        }

        private void Update()
        {
            CleanupDestroyedMines();

            if (activeMines.Count == 0)
            {
                SpawnWave();
            }
        }

        public void SpawnWave()
        {
            CleanupDestroyedMines();

            if (minePrefabs == null || minePrefabs.Length == 0)
            {
                Debug.LogError(
                    "MineSpawner has no mine prefabs assigned.",
                    this
                );

                return;
            }

            while (activeMines.Count < mineCount)
            {
                SpawnRandomMine();
            }

            Debug.Log($"Mine wave spawned: {activeMines.Count} mines.");
        }

        private void SpawnRandomMine()
        {
            MineNode prefab = minePrefabs[
                Random.Range(0, minePrefabs.Length)
            ];

            for (int attempt = 0; attempt < maxSpawnAttempts; attempt++)
            {
                Vector3 position = GetRandomSpawnPosition();

                if (!IsPositionAvailable(position))
                    continue;

                MineNode mine = Instantiate(
                    prefab,
                    position,
                    Quaternion.identity,
                    mineParent
                );

                activeMines.Add(mine);
                return;
            }

            Debug.LogWarning(
                $"Could not find a valid spawn position for mine after {maxSpawnAttempts} attempts.",
                this
            );
        }

        private bool IsPositionAvailable(Vector3 position)
        {
            float minimumDistanceSqr =
                minimumMineDistance * minimumMineDistance;

            foreach (MineNode mine in activeMines)
            {
                if (mine == null)
                    continue;

                float distanceSqr =
                    (mine.transform.position - position).sqrMagnitude;

                if (distanceSqr < minimumDistanceSqr)
                    return false;
            }

            return true;
        }

        private Vector3 GetRandomSpawnPosition()
        {
            if (spawnArea == null)
            {
                Debug.LogError(
                    "MineSpawner has no Spawn Area assigned.",
                    this
                );

                return transform.position;
            }

            Vector3 halfSize = spawnArea.lossyScale * 0.5f;

            float x = Random.Range(-halfSize.x, halfSize.x);
            float y = Random.Range(-halfSize.y, halfSize.y);

            return spawnArea.position + new Vector3(x, y, 0f);
        }

        private void CleanupDestroyedMines()
        {
            activeMines.RemoveAll(mine => mine == null);
        }

        private void OnDrawGizmos()
        {
            if (spawnArea == null)
                return;

            Gizmos.matrix = Matrix4x4.TRS(
                spawnArea.position,
                spawnArea.rotation,
                spawnArea.lossyScale
            );

            Gizmos.DrawWireCube(Vector3.zero, Vector3.one);

            Gizmos.matrix = Matrix4x4.identity;
        }
    }
}