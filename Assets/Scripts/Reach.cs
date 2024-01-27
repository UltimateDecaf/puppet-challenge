using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reach : BaseState
{
    [SerializeField] GameObject cube;
    // Start is called before the first frame update
   private void OnEnable()
    {
        Debug.Log("Reach started");
    }

    // Update is called once per frame
    protected override void Update()
    {
        Debug.Log("Reach updates");
    }

   
}
