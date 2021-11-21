using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SplineMesh {
    public class Obstacle : MonoBehaviour {
        public GameController gameController;
        public ObstacleController obstacleController;

        void OnTriggerEnter(Collider collider) {
            if (collider.gameObject.tag == "Player") {
                gameController.LooseHealth();
                obstacleController.DequeueObstacleFromPool(gameObject);
            }
        }

        public void SetGameController(GameController gc) {
            gameController = gc;
        }

        public void SetObstacleController(ObstacleController oc) {
            obstacleController = oc;
        }

    }
}
