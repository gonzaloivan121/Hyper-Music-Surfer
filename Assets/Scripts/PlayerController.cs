using SplineMesh;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject character;
    public GameObject pivot;
    public GameObject stickman;
    public Camera cam;

    public float rotateSpeed = 5f; // how fast the object should rotate
    public float speedMultiplier = 10f; // how fast the object should rotate

    private Touch touch;

    // Start is called before the first frame update
    void Start() {
        QualitySettings.vSyncCount = 1;
    }

    // Update is called once per frame
    void Update() {
        RotatePlayer();
        CheckScreenOrientation();
        FollowPlayer();
    }

    void RotatePlayer() {
        if (Input.touchCount == 1) {
            touch = Input.GetTouch(0);
            if (touch.phase.Equals(TouchPhase.Moved)) {
                character.transform.Rotate(0f, 0f, touch.deltaPosition.x * Time.deltaTime * rotateSpeed * speedMultiplier);
            }
        }
    }

    void CheckScreenOrientation() {
        if (Screen.orientation == ScreenOrientation.Portrait) {
            SetCamZPosition(-.50f);
        } else if (Screen.orientation == ScreenOrientation.Landscape) {
            SetCamZPosition(0f);
        }
#if UNITY_EDITOR
        if (Screen.height > Screen.width) {
            SetCamZPosition(-.50f);
        } else {
            SetCamZPosition(0f);
        }
#endif
    }

    void FollowPlayer() {
        cam.transform.LookAt((stickman.transform.position + pivot.transform.position) / 2f);
    }

    void SetCamZPosition(float z) {
        cam.transform.localPosition = new Vector3(
            cam.transform.localPosition.x,
            cam.transform.localPosition.y,
            z
        );
    }

}
