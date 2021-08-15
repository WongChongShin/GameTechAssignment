using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerStep : MonoBehaviour
{
    Text stepText;
    public static int stepNumber = 0;
    // Start is called before the first frame update
    void Start()
    {
        stepText = GetComponent<Text>();
        stepText.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        stepText.text = stepNumber.ToString();
    }
}
