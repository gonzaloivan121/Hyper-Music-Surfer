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

    int TapCount;
    public float MaxDoubleTapTime;
    float NewTime;

    public GroundCheck groundCheck;
    public float jumpForce = 20f;
    public float gravity = -9.81f;
    public float gravityScale = 1f;
    float velocity;

    // Start is called before the first frame update
    void Start() {
        QualitySettings.vSyncCount = 1;
        TapCount = 0;
    }

    // Update is called once per frame
    void Update() {
        RotatePlayer();
        UpdateVelocity();
        CheckScreenOrientation();
        FollowPlayer();
    }

    void UpdateVelocity() {
        velocity += gravity * gravityScale * Time.deltaTime;
        if (groundCheck.isGrounded && velocity < 0) {
            //character.transform.position = new Vector3(0, 0, 0);
            velocity = 0;
        }
        CheckDoubleTap();
        character.transform.Translate(new Vector3(0, velocity, 0) * Time.deltaTime, Space.Self);
    }

    void CheckDoubleTap() {
        if (Input.touchCount == 1) {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Ended) {
                TapCount += 1;
            }

            if (TapCount == 1) {
                NewTime = Time.time + MaxDoubleTapTime;
            } else if (TapCount == 2 && Time.time <= NewTime) {
                Jump();
                TapCount = 0;
            }
        }
        if (Time.time > NewTime) {
            TapCount = 0;
        }
    }

    void Jump() {
        velocity = jumpForce;
    }

    void RotatePlayer() {
        if (Input.touchCount == 1) {
            touch = Input.GetTouch(0);
            if (touch.phase.Equals(TouchPhase.Moved)) {
                character.transform.Rotate(0f, 0f, touch.deltaPosition.x * Time.deltaTime * rotateSpeed * speedMultiplier);
            }
        }
#if UNITY_EDITOR
        character.transform.Rotate(0f, 0f, (Input.GetAxis("Mouse X")) * Time.deltaTime * rotateSpeed * speedMultiplier * 5f);
#endif
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
