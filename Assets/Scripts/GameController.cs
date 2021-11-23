using System.Collections;
using System.Collections.Generic;
using TMPro;
using SplineMesh;
using UnityEngine;

public class GameController : MonoBehaviour {
    public int health = 0;
    public PlayerController player;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI speedText;

    private int score = 0;
    private float speed = 0;

    void Start() {
        QualitySettings.vSyncCount = 1;
        Cursor.lockState = CursorLockMode.Locked;
        scoreText.text = "Score: " + score;
        healthText.text = "Health: " + health;
        speedText.text = "Speed: 0";
    }

    void Update() {
        CalculatePlayerSpeed();
        //speedText.text = "Speed: " + Mathf.RoundToInt(speed);
        speedText.text = "Speed: " + speed;
    }

    void CalculatePlayerSpeed() {
        speed = player.CalculateSpeed();
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
