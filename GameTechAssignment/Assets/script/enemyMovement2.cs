using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement2 : MonoBehaviour
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
    Rigidbody player_rigidBody;
    public enemyDistance enemy_distance;

    // Start is called before the first frame update
    void Start()
    {
        grid = GameObject.FindGameObjectsWithTag("grid");
        obstacle = GameObject.FindGameObjectsWithTag("obstacle");
        player = GameObject.FindWithTag("back");
        anim.GetComponent<Animator>();
        player_rigidBody = GetComponent<Rigidbody>();
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
    void objectNearest(GameObject triggerObject)
    {
        for (int i = 0; i < grid.Length; i++)
        {
            if (grid[i].name == triggerObject.name)
            {
                continue;
            }
            else
            {

                if (Vector3.Distance(triggerObject.transform.position, grid[i].transform.position) <= 6)
                {
                    possibleMoveObject.Add(grid[i].name);
                    for (int j = 0; j < obstacle.Length; j++)
                    {
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
    void checkMoveVisited()
    {
        for (int i = 0; i < closeList.Count; i++)
        {
            for (int j = 0; j < possibleMoveObject.Count; j++)
            {
                if (closeList[i].Equals(possibleMoveObject[j]))
                {
                    possibleMoveObject.Remove(possibleMoveObject[j]);
                }
            }
        }
        if (closeList.Count > 1)
        {
            closeList.Remove(closeList[0]);
        }
    }

    void calculateDistance()
    {
        float shortestDist = Vector3.Distance(GameObject.Find(possibleMoveObject[0]).transform.position, player.transform.position);
        int noObjectArray = 0;
        if (possibleMoveObject.Count != 0)
        {
            if (movigNow == false)
            {
                if (possibleMoveObject.Count > 1)
                {
                    for (int i = 1; i < possibleMoveObject.Count; i++)
                    {
                        float otherDistance = Vector3.Distance(GameObject.Find(possibleMoveObject[i]).transform.position, player.transform.position);
                        if (otherDistance < shortestDist)
                        {
                            shortestDist = otherDistance;
                            noObjectArray = i;

                        }
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
                Vector3 enemyDirection = new Vector3(shortestPathObject.transform.position.x, transform.position.y, shortestPathObject.transform.position.z);
                player_rigidBody.MovePosition(transform.position + (shortestPathObject.transform.position - transform.position).normalized * Time.deltaTime * speed);
                transform.LookAt(enemyDirection);
                StartCoroutine(waiting());
            }
        }
    }
    IEnumerator waiting()
    {


        anim.SetBool("isChase", true);
        yield return new WaitUntil(() => Vector3.Distance(shortestPathObject.transform.position, transform.position) < 1);
        movigNow = false;

    }

    public void adjustSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

}
