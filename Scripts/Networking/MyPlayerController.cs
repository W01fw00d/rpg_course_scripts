﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MyPlayerController : NetworkBehaviour {

    public Transform mainCamera;
    public float cameraDistance = 15f;
    public float cameraHeight = 15f;
    public Vector3 cameraOffset;

    [SyncVar]
    public Color myColor;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;

	// Use this for initialization
	void Start () {

        foreach (Transform child in transform)
        {
            if (child.GetComponent<MeshRenderer>() != null)
            {
                child.GetComponent<MeshRenderer>().material.color = myColor;
            }
        }

        cameraOffset = new Vector3(0, cameraHeight, -cameraDistance);
        mainCamera = Camera.main.transform;

        MoveCamera();
	}
	
	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer)
        {
            return;
        }

#if UNITY_EDITOR
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 120f;
        var z = -Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;
#else
        var x = ECMInput.GetAxis("Horizontal") * Time.deltaTime * 120f;
        var z = -ECMInput.GetAxis("Vertical") * Time.deltaTime * 3.0f;
#endif
        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdFireBullet();
        }
#else
        if(ETCInput.GetButtonDown("ButtonFire"))
        {
            CmdFireBullet();
        }

#endif
        MoveCamera();
    }

    void MoveCamera()
    {
        mainCamera.position = transform.position;
        mainCamera.rotation = transform.rotation;
        mainCamera.Translate(cameraOffset);
    }

    [Command]
    void CmdFireBullet()
    {
        var bullet = Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation
        ) as GameObject;

        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 10f;
        bullet.GetComponent<Bullet>().myColor = myColor;
        NetworkServer.Spawn(bullet);
        Destroy(bullet, 2.0f);
    }
}
