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

        void OnEnable() {
            rate = 0;
            spline = GetComponent<Spline>();
        }

        void Start() {
            GeneratePickups();
        }

        void GeneratePickups() {
            for (float i = 0f; i < maxPickups; i += 1f) {
                float mappedRate = i.Map(0f, maxPickups, .0001f, spline.nodes.Count - 1);
                rate = mappedRate;
                CurveSample sample = spline.GetSample(rate);

                GameObject pivot = Instantiate(pickupPrefab, pickupParent);
                pivot.transform.position = sample.location;

                Pickup pickup = pivot.GetComponentInChildren<Pickup>();
                pickup.SetGameController(gameController);

                float randomAngle = Random.Range(0, 359);
                Vector3 randomRot = new Vector3(0f, 0f, randomAngle);

                pivot.transform.Rotate(randomRot);
            }
        }

    }
}

public static class ExtensionMethods {
    public static float Map(this float value, float from1, float to1, float from2, float to2) {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

}
