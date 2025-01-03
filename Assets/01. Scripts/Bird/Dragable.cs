using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dragable : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler
{
    public delegate void OnStartDragEvent();
    public event OnStartDragEvent OnStartDrag;
    
    public delegate void ChangeBirdPosition(Vector3 position);
    public event ChangeBirdPosition OnChangeBird;

    public delegate void ShotBird(Vector3 force);
    public event ShotBird OnShot;

    private Vector3 startPosition;
    private Vector3 endPosition;
    
    private Vector3 force;
    private float SpriteOffset;
    private const float maxPullLength = 25f;
    private const float forceScale = 5f;

    public void OnPointerDown(PointerEventData eventData)
    {
        startPosition = Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, Camera.main.WorldToScreenPoint(eventData.position).z));
        OnStartDrag?.Invoke();
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(new Vector3(
            eventData.position.x, eventData.position.y, Camera.main.WorldToScreenPoint(transform.position).z));
        
        Vector3 difference = newPosition - startPosition;

        if (difference.magnitude > maxPullLength)
        {
            difference = difference.normalized * maxPullLength;
            newPosition = startPosition + difference;
        }

        transform.position = endPosition = newPosition;

        OnChangeBird?.Invoke(newPosition);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        CalcForce();
        
        OnShot?.Invoke(-force);
    }

    private void CalcForce()
    {
        Vector3 forceDirection = endPosition - startPosition;
        
        float scalar = forceDirection.magnitude;
        
        force = forceDirection.normalized * scalar * forceScale;
    }
}
