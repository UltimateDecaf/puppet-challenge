using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Unity.PlasticSCM.Editor.WebApi.CredentialsResponse;

public class GrabController : MonoBehaviour
{

    public Vector2 handleTarget;
    public RectTransform Handle;
    public float time;

    void Start()
    {
        Handle = GetComponent<RectTransform>();
    }

    void Update()
    {
        float c = Mathf.PingPong(Time.time, 2);
        float r = MapValue(c, 0, 2, -1, 1);

        print(r);


        GetComponent<RectTransform>().anchoredPosition =
            Vector2.Lerp(Handle.anchoredPosition, handleTarget * r, Time.time);


    }

    float MapValue(float v, float fromMin, float fromMax, float toMin, float toMax)
    {
        float scaledValue = (v - fromMin) / (fromMax - fromMin);
        float mappedValue = (scaledValue * (toMax - toMin)) + toMin;

        return mappedValue;

    }
}       


    