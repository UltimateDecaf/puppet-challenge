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

   public SplineAnimate slapHand;
   public GameObject reachHand;
   private Vector3 startHandPos;
   private Quaternion startHandRot;

   public SplineExtrude slapArm;

   public static event Action OnSlap;
   
   [Header("References")]
   [SerializeField] private CameraSwitcher cameraSwitcher;

   private void OnEnable()
   {
      reachHand.SetActive(false);
      slapHand.gameObject.SetActive(true);

      // Slap Sequence
      StartCoroutine("SlapSequence");
      slapHand.Play();
      slapArm.gameObject.SetActive(true);
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
         // Extrude Slap Arm
         slapArm.Rebuild();
         armValue = Mathf.Lerp(0, 1, elapsed / duration);
         slapArm.Range = new Vector2(0, armValue);

         elapsed += Time.deltaTime;
         yield return null;
      }
      neutralModel.SetActive(false);
      angryModel.SetActive(true);

      yield return new WaitForSeconds(4f);
      StateManager.Instance.UpdateGameState(StateManager.Instance.DrawState);
      cameraSwitcher.ActivateDrawReachCamera();
   }

   private void OnDisable()
   {
      reachHand.SetActive(true);
      slapArm.gameObject.SetActive(false);
      slapHand.gameObject.SetActive(false);
      angryModel.SetActive(false);
      neutralModel.SetActive(true);
      slapArm.gameObject.SetActive(false);
   }
}
