using UnityEngine;

public class GroundCheck : MonoBehaviour {
    public float distanceToCheck = 0.5f;
    public bool isGrounded;
    public LayerMask layerMask;

    void FixedUpdate() {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, -transform.up/4);
        Debug.DrawRay(ray.origin, ray.direction, Color.green);

        if (Physics.Raycast(ray, out hit, distanceToCheck, layerMask)) {
            isGrounded = true;
        } else {
            isGrounded = false;
        }
    }
}