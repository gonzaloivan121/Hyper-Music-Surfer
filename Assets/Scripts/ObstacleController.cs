using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SplineMesh {
    [RequireComponent(typeof(Spline))]
    public class ObstacleController : MonoBehaviour {
        public Transform pickupParent;
        public GameObject pickupPrefab;
        public GameController gameController;

        public float maxObstacles = 0f;

        private Spline spline;
        private float rate = 0;

        [System.Serializable]
        public class Pool {
            public string tag;
            public GameObject prefab;
            public int size;
        }

        private string obstacleTag = "Obstacle";

        public List<Pool> pools;
        public Dictionary<string, Queue<GameObject>> poolDictionary;
        // Pool Test End

        void OnEnable() {
            rate = 0;
            spline = GetComponent<Spline>();
        }

        void Start() {
            poolDictionary = new Dictionary<string, Queue<GameObject>>();
            EnqueueObstaclesInPool();
            GenerateObstacles();
        }

        public void DequeueObstacleFromPool(GameObject pickup) {
            pickup.SetActive(false);
            poolDictionary[obstacleTag].Dequeue();
        }

        void EnqueueObstaclesInPool() {
            foreach (Pool pool in pools) {
                Queue<GameObject> pickupPool = new Queue<GameObject>();
                for (int i = 0; i < pool.size; i++) {
                    GameObject pickup = Instantiate(pickupPrefab, pickupParent);
                    pickup.SetActive(false);
                    pickupPool.Enqueue(pickup);
                }

                poolDictionary.Add(pool.tag, pickupPool);
            }
        }

        GameObject SpawnFromPool(string tag, Vector3 position, Vector3 rotation) {
            if (!poolDictionary.ContainsKey(tag)) {
                Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
                return null;
            }

            GameObject objectToSpawn = poolDictionary[tag].Dequeue();
            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.Rotate(rotation);

            poolDictionary[tag].Enqueue(objectToSpawn);

            return objectToSpawn;
        }

        void GenerateObstacles() {
            for (float i = 1f; i < maxObstacles; i += 1f) {
                float mappedRate = i.Map(0f, maxObstacles, .0001f, spline.nodes.Count - 1);
                rate = mappedRate;
                CurveSample sample = spline.GetSample(rate);

                float randomAngle = Random.Range(0, 359);
                Vector3 randomRot = new Vector3(0f, 0f, randomAngle);

                GameObject pivot = SpawnFromPool(obstacleTag, sample.location, randomRot);

                Obstacle obstacle = pivot.GetComponentInChildren<Obstacle>();
                obstacle.SetGameController(gameController);
                obstacle.SetObstacleController(this);
            }
        }

    }
}
