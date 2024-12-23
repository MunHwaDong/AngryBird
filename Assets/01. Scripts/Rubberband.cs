using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Rubberband : MonoBehaviour
{
    private LineRenderer rubberband;

    void Start()
    {
        rubberband = this.GetComponent<LineRenderer>();
        
        rubberband.SetPosition(0, transform.position);
        rubberband.SetPosition(1, transform.position);
    }
}
