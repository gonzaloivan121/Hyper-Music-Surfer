using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SplineMesh {
    [RequireComponent(typeof(Spline))]
    public class PickupController : MonoBehaviour {
        public Transform pickupParent;
        public GameObject pickupPrefab;
        public GameController gameController;

        public float maxPickups = 0f;

        private Spline spline;
        private float rate = 0;

        [System.Serializable]
        public class Pool {
            public string tag;
            public GameObject prefab;
            public int size;
        }

        private string pickupTag = "Pickup";

        public List<Pool> pools;
        public Dictionary<string, Queue<GameObject>> poolDictionary;
        // Pool Test End

        void OnEnable() {
            rate = 0;
            spline = GetComponent<Spline>();
        }

        void Start() {
            poolDictionary = new Dictionary<string, Queue<GameObject>>();
            EnqueuePickupsInPool();
            GeneratePickups();
        }

        public void DequeuePickupFromPool(GameObject pickup) {
            pickup.SetActive(false);
            poolDictionary[pickupTag].Dequeue();
        }

        void EnqueuePickupsInPool() {
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

        void GeneratePickups() {
            for (float i = 1f; i < maxPickups; i += 1f) {
                float mappedRate = i.Map(0f, maxPickups, .0001f, spline.nodes.Count - 1);
                rate = mappedRate;
                CurveSample sample = spline.GetSample(rate);

                float randomAngle = Random.Range(0, 359);
                Vector3 randomRot = new Vector3(0f, 0f, randomAngle);

                GameObject pivot = SpawnFromPool(pickupTag, sample.location, randomRot);

                Pickup pickup = pivot.GetComponentInChildren<Pickup>();
                pickup.SetGameController(gameController);
                pickup.SetPickupController(this);
            }
        }

    }
}

public static class ExtensionMethods {
    public static float Map(this float value, float from1, float to1, float from2, float to2) {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

}
