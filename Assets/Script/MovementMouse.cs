using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementMouse : MonoBehaviour
{
    [SerializeField] private GameObject hand;
    [SerializeField] private Vector3 mousePos;
    [SerializeField] private LayerMask mouseCollider;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, mouseCollider))
        {
            mousePos = hitInfo.point;
        }

        hand.transform.position = mousePos;
    }
}
