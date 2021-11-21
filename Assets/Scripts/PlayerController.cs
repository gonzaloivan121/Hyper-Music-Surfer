using SplineMesh;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject character;
    public GameObject pivot;
    public GameObject stickman;
    public GameObject characterHolder;
    private Camera cam;

    public float rotateSpeed = 5f; // how fast the object should rotate
    public float speedMultiplier = 10f; // how fast the object should rotate

    private Touch touch;

    int TapCount;
    public float MaxDoubleTapTime;
    float NewTime;

    private GroundCheck groundCheck;
    public float jumpForce = 20f;
    public float gravity = -9.81f;
    public float gravityScale = 1f;
    float velocity;

    // Start is called before the first frame update
    void Start() {
        TapCount = 0;
        cam = Camera.main;
        groundCheck = characterHolder.GetComponent<GroundCheck>();
    }

    // Update is called once per frame
    void Update() {
        RotatePlayer();
        CheckScreenOrientation();
    }

    void FixedUpdate() {
        UpdateVelocity();
        FollowPlayer();
    }

    void UpdateVelocity() {
        velocity += gravity * gravityScale * Time.deltaTime;

        if (groundCheck.isGrounded && velocity < 0) {
            velocity = 0;
        }

        GetJumpFromTouch();
        GetJumpFromKeyboard();
        GetJumpFromMouse();
        GetJumpFromController();

        if (characterHolder.transform.localPosition.y < -.4226f) {
            characterHolder.transform.Translate(new Vector3(0, -velocity, 0) * Time.deltaTime, Space.Self);
        } else {
            characterHolder.transform.Translate(new Vector3(0, velocity, 0) * Time.deltaTime, Space.Self);
        }

    }

    void GetJumpFromTouch() {
        if (Input.touchCount == 1) {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended) {
                TapCount += 1;
            }

            if (TapCount == 1) {
                NewTime = Time.time + MaxDoubleTapTime;
            } else if (TapCount == 2 && Time.time <= NewTime && groundCheck.isGrounded) {
                Jump();
                TapCount = 0;
            }
        }
        if (Time.time > NewTime) {
            TapCount = 0;
        }
    }

    void GetJumpFromKeyboard() {
        if (Input.GetKeyDown(KeyCode.Space) && groundCheck.isGrounded) {
            Jump();
        }
    }

    void GetJumpFromMouse() {
        if (Input.mousePresent) {
            if (Input.GetMouseButtonDown(0) && groundCheck.isGrounded) {
                Jump();
            }
        }
    }

    void GetJumpFromController() {
        if (Input.GetJoystickNames().Length > 0) {
            if ((Input.GetButtonDown("Jump") || Input.GetAxis("Jump") > 0f) && groundCheck.isGrounded) {
                Jump();
            }
        }
    }

    void Jump() {
        velocity = jumpForce;
    }

    void RotatePlayer() {
        GetRotationFromTouch();
        GetRotationFromController();
        GetRotationFromMouse();
    }

    void GetRotationFromTouch() {
        if (Input.touchCount == 1) {
            touch = Input.GetTouch(0);
            if (touch.phase.Equals(TouchPhase.Moved)) {
                character.transform.Rotate(0f, 0f, touch.deltaPosition.x * Time.deltaTime * rotateSpeed * speedMultiplier);
            }
        }
    }

    void GetRotationFromController() {
        if (Input.GetJoystickNames().Length > 0) {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            if (x != 0.0f || y != 0.0f) {
                float a = (Mathf.Atan2(y, x) * Mathf.Rad2Deg) + 90f;
                Quaternion newRot = Quaternion.Lerp(character.transform.localRotation, Quaternion.Euler(0f, 0f, a), .5f);
                character.transform.localRotation = newRot;
            }
        }
    }

    void GetRotationFromMouse() {
        if (Input.mousePresent) {
            character.transform.Rotate(0f, 0f, (Input.GetAxis("Mouse X")) * Time.deltaTime * rotateSpeed * speedMultiplier * 5f);
        } 
    }

    void CheckScreenOrientation() {
        if (Screen.orientation == ScreenOrientation.Portrait) {
            SetCamZPosition(-1f);
        } else if (Screen.orientation == ScreenOrientation.Landscape) {
            SetCamZPosition(-.5f);
        }

#if UNITY_EDITOR
        if (Screen.height > Screen.width) {
            SetCamZPosition(-1f);
        } else {
            SetCamZPosition(-.5f);
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
