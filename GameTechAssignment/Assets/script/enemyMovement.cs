using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    public float speed;
    GameObject[] grid;
    GameObject[] obstacle;

    // Start is called before the first frame update
    void Start()
    {
        grid = GameObject.FindGameObjectsWithTag("grid");
        obstacle = GameObject.FindGameObjectsWithTag("obstacle");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collide)
    {
        GameObject triggerObject;
        for (int i = 0; i < grid.Length; i++)
        {
            if (collide.gameObject.name == grid[i].gameObject.name)
            {
                triggerObject = grid[i];
                objectNearest(triggerObject);
            }
        }
    }
    void objectNearest(GameObject triggerObject)
    {
        List<string> nameObject = new List<string>();
        for(int i = 0; i < grid.Length; i++)
        {
            if (grid[i].name == triggerObject.name)
            {

            }
            else {
                
                if (Vector3.Distance(triggerObject.transform.position, grid[i].transform.position) <= 6)
                {
                    nameObject.Add(grid[i].name);
                    
                }
            }
        }
    }
}
