using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Rubberband : MonoBehaviour
{
    private LineRenderer rubberband;

    private float maxLength = 10f;

    void Start()
    {
        rubberband = this.GetComponent<LineRenderer>();
        
        rubberband.SetPosition(0, transform.position);
        rubberband.SetPosition(1, transform.position);
    }

    public void PullingRubberband(Vector3 position)
    {
        Vector3 startPosition = rubberband.GetPosition(0);
        
        Vector3 pullingDirection = position - startPosition;

        Vector3 updatePosition = Vector3.zero;

        if (pullingDirection.magnitude > maxLength)
        {
            pullingDirection = pullingDirection.normalized * maxLength;
            position = pullingDirection;
        }
        
        rubberband.SetPosition(1, position);
    }

    public void OscillationBand(Vector3 Force)
    {
        Debug.Log("Oscillation Band");
    }
}
