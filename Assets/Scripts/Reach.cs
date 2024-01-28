using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Splines;
using UnityEngine.SceneManagement;
using System.IO;

public class Reach : BaseState
{
    [SerializeField] public GameObject hand;
    public float moveSpeed;
    public float fastSpeed;
    
    private LineRenderer line;
    
    // Start is called before the first frame update
   private void OnEnable()
    {
        Debug.Log("Reach started");

        line = GameObject.FindGameObjectWithTag("Line").GetComponent<LineRenderer>();
        line.enabled = false;
        GenerateSpline(line);
        
        // Hand OnCollision
        Hand.OnHandCollision += StopAnimation;
        Hand.OnHandCollision += StopExtrusion;
    }

    // Update is called once per frame
    protected override void Update()
    {
       
        
    }

    [Header("Spline Params")]
    public GameObject splinePrefab;
    public float duration;
    public bool extruding = true;
    public TangentMode mode;

    private void GenerateSpline(LineRenderer line)
    {
       // Get the points from the line renderer'
       int pointCount = line.positionCount;
       Vector3[] linePoints = new Vector3[pointCount];
       line.GetPositions(linePoints);

       // Instance a Spline Container
       GameObject prefab = Instantiate(splinePrefab, line.transform.position, Quaternion.Euler(0,0,0)); 
       SplineContainer splineContainer = prefab.GetComponent<SplineContainer>();

       // Convert Line Renderer points into BezierKnots
       List<BezierKnot> lineKnots = new List<BezierKnot>();
       
       foreach (var point in linePoints)
       {
           lineKnots.Add(new BezierKnot(point));
       }
       splineContainer.Spline.Knots = lineKnots;
       splineContainer.Spline.SetTangentMode(mode);
       
       //Extrude Mesh
       extruder = prefab.GetComponent<SplineExtrude>();
       StartCoroutine(ExtrudeArm(extruder));
       
       //Animate Hand Motion
       AnimateHand(splineContainer);
    }

    private SplineExtrude extruder;
    private float lerpVal;

    IEnumerator ExtrudeArm(SplineExtrude extruder)
    {
        const float from = 0;
        const float to = 1;
        float timeElapsed = 0;

        float lerpValue;
        
        while (timeElapsed < duration && extruding)
        {
            lerpValue = Mathf.Lerp(from, to, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            extruder.Range = new Vector2(0, lerpValue);
            extruder.Rebuild();
            yield return null;
        }
        extruder.Range = new Vector2(0, 1);
    }

    private SplineAnimate handAnim;

    public void AnimateHand(SplineContainer container)
    {
        handAnim = hand.GetComponent<SplineAnimate>();
        handAnim.Container = container;
        handAnim.Duration = duration;
        handAnim.Play();
    }

    private void StopAnimation()
    {
        handAnim.Pause();
    }

    private void StopExtrusion()
    {
        extruding = false;
        extruder.enabled = false;
    }
    

    private void OnDisable()
    {
        Hand.OnHandCollision -= StopAnimation;
        Hand.OnHandCollision -= StopExtrusion;
    }
}
