using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float speed = 10;
    public float rotSpeed = 5;
    public GameObject player;
    
    private Quaternion startRot;

    void Start()
    {
        startRot = gameObject.transform.rotation;
        
    }
    public void Move()
    {
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        float xRot = Input.GetAxis("xRot");

        Vector3 forward = gameObject.transform.TransformDirection(Vector3.forward);
        
        forward.y = 0f;
        forward = forward.normalized;

        Vector3 right = new Vector3(forward.z, 0f, -forward.x);
        Vector3 movement = horiz * right + vert * forward;

        //If player is in editor, rotate around camera pos, else rotate around "player" pos
        if (PlayerController.isInEditor)
        {
            transform.RotateAround(transform.position, Vector3.up, xRot * rotSpeed);
        }
        else
        {
            player.transform.RotateAround(player.transform.position, player.transform.up, xRot * rotSpeed);
        }
        
        transform.position += movement * speed * Time.deltaTime;
    }

    void Update()
    {
        if (Input.GetKeyUp("r") && !PlayerController.isInEditor)
        {
            HardResetRot();
        }
    }

    public void HardResetRot()
    {
        transform.rotation = startRot;
    }
}
