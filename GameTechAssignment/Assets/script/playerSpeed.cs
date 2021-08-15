using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerSpeed : MonoBehaviour
{
    Text speedText;
    // Start is called before the first frame update
    void Start()
    {
        speedText = GetComponent<Text>();
        speedText.text = "0";
    }

    public void textValue(float value)
    {
        speedText.text = value.ToString("F2");
    }
}
