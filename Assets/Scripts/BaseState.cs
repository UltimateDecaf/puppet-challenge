using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/* Created by Lari Basangov
 * 
 * This class is a blueprint for states that will inherit
 * from it.
 *
 */
public abstract class BaseState : MonoBehaviour
{
    protected virtual void Start()
    { }
    protected virtual void Update()
    { }
}
