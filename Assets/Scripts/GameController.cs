using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour {
    public int score = 0;
    public PlayerController player;
    public TextMeshProUGUI scoreText;

    public bool test;

    // Start is called before the first frame update
    void Start() {
        QualitySettings.vSyncCount = 1;
        Cursor.lockState = CursorLockMode.Locked;
        scoreText.text = "" + score;
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void UpdateScore(int points) {
        score += points;
        scoreText.text = "" + score;
    }
}
