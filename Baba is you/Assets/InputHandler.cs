using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public Vector3 _userInput { get; set; }
    public float _fieldWidth { get; set; }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Checks for directional input to move the current you character
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _userInput = new Vector3(-_fieldWidth, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _userInput = new Vector3(_fieldWidth, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _userInput = new Vector3(0, _fieldWidth, 0);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _userInput = new Vector3(0, -_fieldWidth, 0);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            _userInput = new Vector3(0, 0, 1);
        }
    }
}
