using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dragable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public delegate void ChangeBirdPosition(Vector3 position);
    public event ChangeBirdPosition OnChangeBird;

    public delegate void ShotBird(Vector3 force);
    public event ShotBird OnShot;

    private Vector3 startPosition;
    private Vector3 force;

    void Awake()
    {
        startPosition = transform.position;
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(new Vector3(
            eventData.position.x, eventData.position.y, Camera.main.WorldToScreenPoint(transform.position).z));

        transform.position = newPosition;
        
        CalcForce(startPosition, newPosition);

        OnChangeBird?.Invoke(newPosition);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnShot?.Invoke(-force);
    }

    private void CalcForce(Vector3 v1, Vector3 v2)
    {
        Vector3 forceDirection = v2 - v1;
        
        float scalar = forceDirection.magnitude;
        
        force = forceDirection.normalized * scalar;
    }
}
