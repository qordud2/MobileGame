using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputManager : MonoBehaviour
{
    public static InputManager Instance
    {
        get
        {
            if(i_instance == null)
            {
                i_instance = FindObjectOfType<InputManager>();
            }
            return i_instance;
        }
    }
    public static InputManager i_instance;

    private string xAxisName = "Horizontal";
    private string yAxisName = "Vertical";
    private string attackName = "Fire1";

    public float xMove { get; private set; }
    public float yMove { get; private set; }
    public bool attack { get; private set; }
    public bool mouseRightBtn { get; private set; }

    void Update()
    {
        xMove = Input.GetAxis(xAxisName);
        yMove = Input.GetAxis(yAxisName);
        attack = Input.GetButtonDown(attackName);
        mouseRightBtn = Input.GetMouseButtonDown(1);
    }
}



