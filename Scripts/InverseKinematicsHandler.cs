using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverseKinematicsHandler : MonoBehaviour {

    private Animator animator;

    public Transform LeftFoot, rightFoot;

    //  USED FOR MANUAL TESTING no se compila!!!
    #if UNITY_EDITOR

    #region TESTING
    public Transform leftIKTarget, rightIKTarget;
    public Transform hintLeft, hintRight;
    public float ikWeight = 1.0f;
    #endregion

    [Header("valores dinámicos de IK")]
    public Vector3 leftFootPosition, rightFootPosition;
    public Quaternion leftFootRotation, rightFootRotation;
    public float leftFootWeight, rightFootWeight;

    [Header("más valores dinámicos de IK")]
    public float offsetY;
    public float lookIkWeight = 1.0f;
    public float bodyWEight = 0.7f;
    public float headWEight = 0.9f;
    public float eyesWEight = 1.0f;
    public float clampWEight = 1.0f;

    public Transform lookPosition;

    #endif



    void Start () {
        animator = GetComponent<Animator>();

        LeftFoot = animator.GetBoneTransform(HumanBodyBones.LeftFoot);
        rightFoot = animator.GetBoneTransform(HumanBodyBones.RightFoot);
    }
	
	void Update () {
		
	}
}
