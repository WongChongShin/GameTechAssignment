using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float speed=0;
    public Animator anim;
    GameObject[] grid;
    GameObject[] enemy;
    Rigidbody player_rigidBody;
    bool die = false;


    // Start is called before the first frame update
    void Start()
    {
        anim.GetComponent<Animator>();
        grid = GameObject.FindGameObjectsWithTag("grid");
        player_rigidBody = GetComponent<Rigidbody>();
        enemy= GameObject.FindGameObjectsWithTag("enemy");

    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput,0f , verticalInput);
        direction.Normalize();
        //transform.Translate(direction * speed * Time.deltaTime);
        player_rigidBody.MovePosition(transform.position + direction * Time.deltaTime * speed);

        if (direction != Vector3.zero)
        {
            transform.forward = direction;
        }
        if (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"))
        {
            anim.SetBool("isRun", true);
        }
        else
        {
            anim.SetBool("isRun", false);
        }
    }
    public void adjustSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    void OnTriggerEnter(Collider collide)
    {
        for (int i = 0; i < grid.Length; i++)
        {
            if (collide.gameObject.name == grid[i].gameObject.name)
            {
                playerStep.stepNumber++;
            }
        }
    }
    void OnCollisionEnter(Collision collide)
    {
        for (int i = 0; i < enemy.Length; i++)
        {
            if (collide.gameObject.name == enemy[i].gameObject.name)
            {
                die = true;
            }
        }
    }
    void OnGUI()
    {
        if (die==true)
        {
            GUIStyle textStyle = new GUIStyle();
            textStyle.fontSize = 20;
            textStyle.normal.textColor = Color.black;
            GUI.Label(new Rect(.5f * Screen.width, .5f * Screen.height, .5f * Screen.width, .5f * Screen.height), "You are lose", textStyle);
        }
    }

}
