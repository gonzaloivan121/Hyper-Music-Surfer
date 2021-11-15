using UnityEngine;

public class GroundCheck : MonoBehaviour {
    public float distanceToCheck = 0.5f;
    public bool isGrounded;
    public LayerMask layerMask;

    private void Update() {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector2.down);
        if (Physics.Raycast(ray, distanceToCheck, layerMask)) {
            isGrounded = true;
        } else {
            isGrounded = false;
        }
    }
}