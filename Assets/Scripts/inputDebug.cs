using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inputDebug : MonoBehaviour {

    public string[] keycode;
    Text inputTxt;

    // Use this for initialization
    void Start()
    {
        inputTxt = gameObject.GetComponent<Text>();
    }



    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < keycode.Length -1; i++)
        {
            if (Input.GetKeyUp((KeyCode)System.Enum.Parse(typeof(KeyCode), keycode[i])))
            {
                inputTxt.text = keycode[i];

               
            }
        }
    }
}
