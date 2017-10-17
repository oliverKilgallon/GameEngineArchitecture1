﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 10;
    public float rotSpeed = 5;
    public GameObject player;
    private Vector3 startPos;
    private Quaternion startRot;

	void Start ()
    {

	}
	
	void Update ()
    {
        ProcessInputs();
        if (player.GetComponent<PlayerController>().getIsInEditor().Equals(true))
        {
            gameObject.GetComponent<Movement>().Move();
        }
    }

    public void ResetPos()
    {
        transform.position = startPos;
        transform.rotation = startRot;
    }

    void ProcessInputs()
    {
        if (Input.GetKeyDown("f") && player.GetComponent<PlayerController>().getIsInEditor().Equals(false))
        {
            startPos = transform.position;
            startRot = transform.rotation;
        }
        if (Input.GetKeyDown("f") && player.GetComponent<PlayerController>().getIsInEditor().Equals(true))
        {
            ResetPos();
        }
    }
}
