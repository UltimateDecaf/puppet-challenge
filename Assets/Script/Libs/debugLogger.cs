using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debugLogger
{
    /* [Object script is attached to][class called from][function called from]
     * |Custom Class||Function|=path to the object that created it=
     */
    public string prefix; //The string that will be put in front of every console log
    public bool debug; //Toggle for whether to log

    public debugLogger() //Making the debug logger
    {
        //Defualt values
        prefix = "[err][err][err]"; 
        debug = false;
    }

    public debugLogger(string pre, bool dbg = true) //If parameters are given
    {
        prefix = pre;
        debug = dbg;
    }

    public void log(string message, string pre = "") //Works like Debug.log but adds the formatted path to the object infront
    {
        if (debug)
        {
            Debug.Log($"{prefix}{pre} {message}");
        }
    }

    public void CheckNull(bool isNull, string obj, string pre) //Easy functions to log whether an object exists
    {
        if (isNull)
        {
            log($"{obj} was not found", pre);
        }
        else
        {
            log($"{obj} was succesfully found", pre);
        }
    }

    public void UpdatePrefix(string pre)
    {
        prefix = pre;
    }
}
