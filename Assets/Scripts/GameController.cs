using System.Collections;
using System.Collections.Generic;
using TMPro;
using SplineMesh;
using UnityEngine;

public class GameController : MonoBehaviour {
    private int score = 0;

    public int health = 0;

    public PlayerController player;
    public ExampleFollowSpline followSplineController;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI speedText;

    void Start() {
        QualitySettings.vSyncCount = 1;
        Cursor.lockState = CursorLockMode.Locked;
        scoreText.text = "Score: " + score;
        healthText.text = "Health: " + health;
        speedText.text = "Speed: 0";
    }

    void Update() {
        speedText.text = "Speed: " + followSplineController.GetSpeed();
    }

    public void UpdateScore(int points) {
        score += points;
        scoreText.text = "Score: " + score;
    }

    public void LooseHealth() {
        health--;
        healthText.text = "Health: " + health;
    }

}
