using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingShotController : MonoBehaviour
{
    private LineRenderer[] rubberbands;

    private Dragable bird;

    void Start()
    {
        rubberbands = GetComponentsInChildren<LineRenderer>();
        
        bird = FindObjectOfType<Dragable>();

        bird.OnChangeBird += UpdateRubberband;
    }

    private void UpdateRubberband(Vector3 position)
    {
        foreach (var rubberband in rubberbands)
        {
            rubberband.SetPosition(1, position);
        }
    }
}
