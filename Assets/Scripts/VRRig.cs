using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

// match the VR headset and controllers
public class VRMap
{
    public Transform vrTarget;
    public Transform rigTarget;
    public Vector3 trackingPositionOffset;
    public Vector3 trackingRotationOffset;

    // set the position and rotation of the rig target to be the VR target
    public void Map()
    {
        rigTarget.position = vrTarget.TransformPoint(trackingPositionOffset);
        rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
    }
}
public class VRRig : MonoBehaviour
{
    public float turnSmoothness = 1f;
    public VRMap head;
    public VRMap leftHand;
    public VRMap rightHand;

    public Transform headConstraint;
    private Vector3 headBodyOffset;

    // Start is called before the first frame update
    void Start()
    {
        headBodyOffset = transform.position - headConstraint.position; // Calculated the position offset bewteen head and body
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //body can be moved or rotated with the head
        transform.position = headConstraint.position + headBodyOffset; 
        transform.forward = Vector3.Lerp(transform.forward, 
            Vector3.ProjectOnPlane(headConstraint.up, Vector3.up).normalized, Time.deltaTime * turnSmoothness); 
        
        head.Map();
        leftHand.Map();
        rightHand.Map();
    }
}
