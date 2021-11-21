using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SplineMesh {
    public class Pickup : MonoBehaviour {
        public int points = 25;
        public GameController gameController;
        public PickupController pickupController;

        void OnTriggerEnter(Collider collider) {
            if (collider.gameObject.tag == "Player") {
                gameController.UpdateScore(points);
                pickupController.DequeuePickupFromPool(gameObject);
            }
        }

        public void SetGameController(GameController gc) {
            gameController = gc;
        }

        public void SetPickupController(PickupController pc) {
            pickupController = pc;
        }

    }
}
