using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {
    public int points = 25;
    public GameController gameController;

    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Player") {
            gameController.UpdateScore(points);
            Destroy(gameObject);
        }
    }

    public void SetGameController(GameController gc) {
        gameController = gc;
    }
}
