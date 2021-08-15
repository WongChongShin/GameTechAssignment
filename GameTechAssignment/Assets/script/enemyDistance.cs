using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyDistance : MonoBehaviour
{
    Text distanceText;
    // Start is called before the first frame update
    void Start()
    {
        distanceText = GetComponent<Text>();
        distanceText.text = "0";
    }

    public void displayDistance(float distanceNumber)
    {
        distanceText.text = distanceNumber.ToString("F1");
    }
}
