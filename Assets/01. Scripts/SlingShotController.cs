using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlingShotController : MonoBehaviour
{
    private BirdSpawner birdSpawner;
    private LineRenderer[] rubberbands;
    private Dragable bird;

    private float maxLength = 10f;

    void Awake()
    {
        rubberbands = GetComponentsInChildren<LineRenderer>();
        birdSpawner = FindObjectOfType<BirdSpawner>();

        birdSpawner.onBirdSpawned += Init;
    }

    void Init(Dragable drag)
    {
        bird = drag;
        
        bird.OnChangeBird += UpdateRubberband;
        bird.OnShot += OscillationBand;
    }

    private void UpdateRubberband(Vector3 position)
    {
        foreach (var rubberband in rubberbands)
        {
            rubberband.SetPosition(1, position);
        }
    }
    
    private void OscillationBand(Vector3 force)
    {
        StartCoroutine(LineAction(force));
    }

    IEnumerator LineAction(Vector3 force)
    {
        float k = 10f; // 탄성 계수 (고무줄의 장력 정도)
        float damping = 0.9f; // 속도 감쇠 계수 (에너지 소실)
        float dt = 0.02f; // 시간 간격

        Vector3 velocity = force;

        Vector3 position = rubberbands[0].GetPosition(1);
        Vector3 anchorPosition = rubberbands[0].GetPosition(0);

        while (true)
        {
            Vector3 displacement = position - anchorPosition;

            float displacementMagnitude = displacement.magnitude;

            Vector3 springForce = Vector3.zero;

            Vector3 gravity = Physics.gravity * dt;

            if (displacementMagnitude > 0.1f)
            {
                springForce = -k * displacement;
            }

            velocity += (springForce + gravity) * dt;
            velocity *= damping;

            position += velocity * dt;

            if (displacementMagnitude >= 20f)
            {
                position = anchorPosition + displacement.normalized * maxLength;
                velocity = -velocity * damping;
            }

            // LineRenderer 끝점 업데이트
            rubberbands[0].SetPosition(1, position);
            rubberbands[1].SetPosition(1, position);

            // 속도가 매우 작아지고 장력이 0이면 자연스럽게 축 늘어짐
            if (velocity.magnitude < 0.01f && displacementMagnitude < 0.1f)
            {
                break;
            }

            yield return null;
        }
    }
}
