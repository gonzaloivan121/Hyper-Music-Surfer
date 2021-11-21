using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SplineMesh {

    [RequireComponent(typeof(Spline))]
    public class ExampleFollowSpline : MonoBehaviour {
        private GameObject generated;
        private Spline spline;
        private float rate = 0;
        private float speed;
        private float rateFactor = 500f;

        public GameObject Follower;

        public float minSpeed;
        public float maxSpeed;

        private void OnEnable() {
            rate = 0;
            string generatedName = "Player";
            var generatedTranform = transform.Find(generatedName);
            generated = generatedTranform != null ? generatedTranform.gameObject : Instantiate(Follower, gameObject.transform);
            generated.name = generatedName;

            spline = GetComponent<Spline>();
            speed = minSpeed;
        }

        void FixedUpdate() {
            MovePlayer();
        }

        void MovePlayer() {
            if (speed < maxSpeed) {
                speed += Time.deltaTime;
            }

            rate += Time.deltaTime * (speed / rateFactor);

            if (rate > spline.nodes.Count - 1) {
                rate -= spline.nodes.Count - 1;
                speed = minSpeed;
            }

            PlaceFollower();
        }

        private void PlaceFollower() {
            if (generated != null) {
                CurveSample sample = spline.GetSample(rate);
                generated.transform.localPosition = sample.location;
                generated.transform.localRotation = sample.Rotation;
            }
        }
    }
}
