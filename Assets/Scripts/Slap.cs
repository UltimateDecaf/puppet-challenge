using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Splines;

public class Slap : BaseState
{
   public Quaternion targetRotation;
   public Vector3 targetPosition;

   [FormerlySerializedAs("duartion")] public float duration;

   public GameObject angryModel;
   public GameObject neutralModel;

   public GameObject slapHand;
   public GameObject reachHand;
   private Vector3 startHandPos;
   private Quaternion startHandRot;

   public SplineExtrude slapArm;

   public static event Action OnSlap;
   
   [Header("References")]
   [SerializeField] private CameraSwitcher cameraSwitcher;

   private void OnEnable()
   {
      startHandPos = reachHand.transform.position;
      startHandRot = reachHand.transform.rotation;
      
      targetPosition = slapHand.transform.position;
      targetRotation = slapHand.transform.localRotation;
      
      // Slap Sequence
      StartCoroutine("SlapSequence");
   }

   protected override void Update()
   {
      
   }

   public IEnumerator SlapSequence()
   {
      float elapsed = 0f;
      float armValue = 0f;

      while (elapsed < duration)
      {
         // Lerp Position
         reachHand.transform.position = Vector3.Lerp(reachHand.transform.position, targetPosition, elapsed / duration);
         
         // Lerp Rotation - This is broken but is way funnier
         Vector3 rot = Vector3.Lerp(reachHand.transform.rotation.eulerAngles, targetRotation.eulerAngles, elapsed / duration);
         reachHand.transform.rotation = quaternion.Euler(rot.x, rot.y, rot.y);
         
         // Extrude Slap Arm
         slapArm.Rebuild();
         armValue = Mathf.Lerp(0, 1, elapsed / duration);
         slapArm.Range = new Vector2(0, armValue);

         elapsed += Time.deltaTime;
         yield return null;
      }
      OnSlap?.Invoke();
      neutralModel.SetActive(false);
      angryModel.SetActive(true);

      yield return new WaitForSeconds(4f);
      StateManager.Instance.UpdateGameState(StateManager.Instance.DrawState);
      ResetHandPosition();
      cameraSwitcher.ActivateDrawReachCamera();
   }

   void ResetHandPosition()
   {
      reachHand.transform.position = startHandPos;
      reachHand.transform.rotation = startHandRot;
   }

   private void OnDisable()
   {
      angryModel.SetActive(false);
      neutralModel.SetActive(true);
      slapArm.gameObject.SetActive(false);
   }
}
