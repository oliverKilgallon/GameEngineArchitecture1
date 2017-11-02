using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float speed = 10;
    public float rotSpeed = 5;
    public GameObject player;
    
    private Quaternion startRot;

    //private bool resetRot = false;
    //private float resetSpeed = 1f;

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
        
        if (PlayerController.isInEditor)
        {
            transform.RotateAround(transform.position, Vector3.up, xRot * rotSpeed);
        }
        else
        {
            transform.RotateAround(player.transform.position, Vector3.up, xRot * rotSpeed);
        }
        
        transform.position += movement * speed * Time.deltaTime;
    }

    void Update()
    {
        if (Input.GetKeyUp("r") && !PlayerController.isInEditor)
        {
            HardResetRot();
        }
        //if (Input.GetKeyUp("r") && !rotating)
        //{
        //    if (gameObject.GetComponent<PlayerController>() == null)
        //    {
        //        if (gameObject.name.Equals("Main Camera"))
        //        {
        //            gameObject.GetComponent<Movement>().ResetRot();
        //        }
        //    }
        //    if (gameObject.GetComponent<PlayerController>() != null)
        //    {
        //        gameObject.GetComponent<Movement>().ResetRot();
        //    }
        //}
        
        //if (rotating.Equals(true))
        //{
        //    if (!transform.rotation.Equals(startRot))
        //    {
        //        transform.rotation = Quaternion.Lerp(transform.rotation, startRot, resetSpeed * (Time.time % 1));
        //    }
        //    else
        //    {
        //        rotating = false;
        //    }
        //}
    }
    //public void ResetRot()
    //{
    //    rotating = true;
    //}

    public void HardResetRot()
    {
        transform.rotation = startRot;
    }
}
