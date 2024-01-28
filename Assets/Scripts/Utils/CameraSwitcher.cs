using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using static UnityEditor.SceneView;


/* This script handles switching and transitioning from different Cinemachine Virtual cameras.
 *
 */
public class CameraSwitcher: MonoBehaviour
{
    public CinemachineVirtualCamera DrawReachCamera;
  //  public CinemachineVirtualCamera GrabCamera;
    public CinemachineVirtualCamera SlapWinCamera;

    private const int maxPriority = 15;
    private const int noPriority = 5;

    public void ActivateDrawReachCamera()
    {
        DrawReachCamera.Priority = maxPriority;
  //      GrabCamera.Priority = noPriority;
        SlapWinCamera.Priority = noPriority;
    }

    public void ActivateGrabCamera()
    {
        DrawReachCamera.Priority = noPriority;
   //     GrabCamera.Priority = maxPriority;
        SlapWinCamera.Priority = noPriority;
    }

    public void ActivateSlapWinCamera()
    {
        DrawReachCamera.Priority = noPriority;
    //    GrabCamera.Priority = noPriority;
        SlapWinCamera.Priority = maxPriority;
    }


}
