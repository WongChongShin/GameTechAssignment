using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    public float speed;
    GameObject[] grid;
    GameObject[] obstacle;
    GameObject player;
    List<string> possibleMoveObject = new List<string>();
    List<string> closeList = new List<string>();
    GameObject shortestPathObject;
    public Animator anim;
    bool movigNow = false;
    Vector3 lookDirection;
    Rigidbody enemy_rigidBody;
    public enemyDistance enemy_distance;
    GameObject playerCollide;

    // Start is called before the first frame update
    void Start()
    {
        grid = GameObject.FindGameObjectsWithTag("grid");
        obstacle = GameObject.FindGameObjectsWithTag("obstacle");
        player = GameObject.FindWithTag("front");
        anim.GetComponent<Animator>();
        enemy_rigidBody = GetComponent<Rigidbody>();
        playerCollide = GameObject.FindWithTag("player");
    }

    // Update is called once per frame
    void Update()
    {
        calculateDistance();
    }

    void OnTriggerEnter(Collider collide)
    {
        possibleMoveObject.Clear();
        anim.SetBool("isChase", false);
        for (int i = 0; i < grid.Length; i++)
        {
            if (collide.gameObject.name == grid[i].gameObject.name)
            {
                closeList.Add(grid[i].name);
                objectNearest(grid[i]);
            }
        }
    }

    //check which grid is near to the enemy standing grid
    void objectNearest(GameObject triggerObject)
    {
        for(int i = 0; i < grid.Length; i++)
        {
            if (grid[i].name == triggerObject.name)
            {
                continue;
            }
            else {
                //check the distance between the grid enemy standing and the grid surrounding
                if (Vector3.Distance(triggerObject.transform.position, grid[i].transform.position) <= 6)
                {
                    possibleMoveObject.Add(grid[i].name);
                    for (int j = 0; j < obstacle.Length; j++)
                    {
                        //check the distance between the surrounding and the obstacle
                        if (Vector3.Distance(obstacle[j].transform.position, grid[i].transform.position) <= 4)
                        {
                            possibleMoveObject.Remove(grid[i].name);
                        }
                    }

              
                }
            }
        }
        checkMoveVisited();
    }

    //check whether a move is visited before or not
    void checkMoveVisited()
    {
        for (int i = 0; i < closeList.Count; i++)
        {
            for (int j = 0; j < possibleMoveObject.Count; j++)
            {
                if (closeList[i].Equals(possibleMoveObject[j]))
                {   
                    //delete the object inside the open list is the object are visited before
                    possibleMoveObject.Remove(possibleMoveObject[j]);
                }
            }
        }
        if (closeList.Count > 1)
        {
            //delete first item if the close list have two and more object
            closeList.Remove(closeList[0]);
        }
    }
    
    //best first search algorithm
    //calculate the distance between enemy and player
    void calculateDistance()
    {
        float shortestDist = Vector3.Distance(GameObject.Find(possibleMoveObject[0]).transform.position, player.transform.position);
        int noObjectArray = 0;
        if (possibleMoveObject.Count != 0)
        {
            if (movigNow == false)
            {
                //check whether open list have store two object and above
                if (possibleMoveObject.Count > 1)
                {
                    for (int i = 1; i < possibleMoveObject.Count; i++)
                    {
                        float otherDistance = Vector3.Distance(GameObject.Find(possibleMoveObject[i]).transform.position, player.transform.position);
                        //compare the distance for the object in open list after caculation the distance between enemy and player
                        if (otherDistance < shortestDist)
                        {
                            shortestDist = otherDistance;
                            noObjectArray = i;

                        }
                        //store the grid that is shortest distance into a new game object
                        shortestPathObject = GameObject.Find(possibleMoveObject[noObjectArray]);
                    }
                }
                else
                {
                    shortestPathObject = GameObject.Find(possibleMoveObject[noObjectArray]);
                }
                movigNow = true;
                enemy_distance.displayDistance(shortestDist);
            }
            else
            {
                //move enemy through the shortest path to the player
                Vector3 enemyDirection = new Vector3(shortestPathObject.transform.position.x, transform.position.y, shortestPathObject.transform.position.z);
                enemy_rigidBody.MovePosition(transform.position +(shortestPathObject.transform.position - transform.position).normalized * Time.deltaTime * speed);
                transform.LookAt(enemyDirection);
                StartCoroutine(waiting());
            }
        }
    }
    IEnumerator waiting()
    {
        
        //wait for enemy arrive the grid that is shortest path
        anim.SetBool("isChase", true);  
        yield return new WaitUntil(()=>Vector3.Distance(shortestPathObject.transform.position, transform.position) < 1);
        movigNow = false;

    }

    public void adjustSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
    void OnCollisionEnter(Collision collide)
    {
        if (collide.gameObject.name == playerCollide.gameObject.name)
        {
            enemy_rigidBody.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

}
