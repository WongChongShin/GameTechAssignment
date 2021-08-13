using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float speed=0;
    public Animator anim;
 

    // Start is called before the first frame update
    void Start()
    {
        anim.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput,0f , verticalInput);
        direction.Normalize();
        transform.Translate(direction * speed * Time.deltaTime,Space.World);

        ////if (direction != Vector3.zero)
        ////{
        ////    transform.forward=direction;
        ////}
        ////if(Input.GetKey("w")|| Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"))
        ////{
        ////    anim.SetBool("isRun", true);
        ////}
        ////else
        ////{
        ////    anim.SetBool("isRun", false);
        ////}
    }
    public void adjustSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

}
