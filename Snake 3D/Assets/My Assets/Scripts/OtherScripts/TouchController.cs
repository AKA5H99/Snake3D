using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    public SnakeController SnakeControllerScript;
    public FixedTouchField_Android FixedTouchFieldScript;
    private float halfScreenWidth;
    public float TouchRotationSpeed = 3f,TouchRotation;

    private void Start()
    {
        halfScreenWidth = Screen.width / 2;
    }
    private void Update()
    {
        NewTouchController();
    }

    public void NewTouchController()
    {
        if (FixedTouchFieldScript.XPos > halfScreenWidth)
        {
            TouchRotation += TouchRotationSpeed * Time.deltaTime;
            SnakeControllerScript.TouchDirection = Mathf.Clamp(TouchRotation, -1, 1);
        }
        else if(FixedTouchFieldScript.XPos == 0)
        {
            SnakeControllerScript.TouchDirection = 0;
            TouchRotation = 0;
        }
        else if (FixedTouchFieldScript.XPos < halfScreenWidth)
        {
            TouchRotation -= TouchRotationSpeed * Time.deltaTime;
            SnakeControllerScript.TouchDirection = Mathf.Clamp(TouchRotation, -1, 1);
        }
    }
}
