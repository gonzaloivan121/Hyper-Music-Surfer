using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKFootPlacement : MonoBehaviour {

    Animator anim;

    public LayerMask layerMask;

    [Range (0, 1f)]
    public float DistanceToGround;

    // Start is called before the first frame update
    private void Start() {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update() {
        
    }

    private void OnAnimatorIK(int layerIndex) {
        if (anim) {
            anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1f);
            anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1f);

            // Left Foot
            RaycastHit hit;
            Ray ray = new Ray(anim.GetIKPosition(AvatarIKGoal.LeftFoot) + Vector3.up, Vector3.down);
            Debug.DrawRay(anim.GetIKPosition(AvatarIKGoal.LeftFoot) + Vector3.up, ray.direction, Color.green);

            if (Physics.Raycast(ray, out hit, DistanceToGround + 1f, layerMask)) {
                if (hit.transform.tag == "Skateboard") {
                    Vector3 footPosition = hit.point;
                    footPosition.y += DistanceToGround;
                    anim.SetIKPosition(AvatarIKGoal.LeftFoot, footPosition);
                    anim.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.LookRotation(transform.forward, hit.normal));
                }
            }

            ray = new Ray(anim.GetIKPosition(AvatarIKGoal.RightFoot) + Vector3.up, Vector3.down);
            Debug.DrawRay(anim.GetIKPosition(AvatarIKGoal.RightFoot) + Vector3.up, ray.direction, Color.green);

            if (Physics.Raycast(ray, out hit, DistanceToGround + 1f, layerMask)) {
                if (hit.transform.tag == "Skateboard") {
                    Vector3 footPosition = hit.point;
                    footPosition.y += DistanceToGround;
                    anim.SetIKPosition(AvatarIKGoal.RightFoot, footPosition);
                    anim.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.LookRotation(transform.forward, hit.normal));
                }
            }
        }
    }
}
