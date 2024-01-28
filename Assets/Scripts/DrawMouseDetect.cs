using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawMouseDetect : MonoBehaviour
{
    public bool enter = false;
    public bool exit = false;

    private void OnMouseEnter()
    {
        enter = true;
        exit = false;
    }
    private void OnMouseExit()
    {
        enter = false;
        exit = true;
    }
}
