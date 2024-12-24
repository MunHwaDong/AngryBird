using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    private float maxX, minX, maxY, minY;
    
    private BirdSpawner _birdSpawner;
    private Dragable _bird;
    private Coroutine _coroutine;

    private const float movementTime = 3f;
    private const float speed = 2f;
    private Camera cam;
    
    void Start()
    {
        cam = GetComponent<Camera>();
        _birdSpawner = FindObjectOfType<BirdSpawner>();

        _birdSpawner.onBirdSpawned += ReturnToSpawnPoint;
        
        maxX = spriteRenderer.bounds.max.x - cam.orthographicSize;
        maxY = spriteRenderer.bounds.max.y - cam.orthographicSize;
        minX = spriteRenderer.bounds.min.x + cam.orthographicSize;
        minY = spriteRenderer.bounds.min.y + cam.orthographicSize;
        
        EventBus.RegisterEvent(EventType.COLLISION, () => { StopCoroutine(_coroutine); });
    }

    void ReturnToSpawnPoint(Dragable bird)
    {
        //이전에 날렸던 새의 관찰을 그만하고, 새로운 새의 움직임이 시작되었는지 관찰한다.
        if (_bird != null) _bird.OnShot -= CameraMove;
        
        _bird = bird;
        _bird.OnShot += CameraMove;
        
        StartCoroutine(LerpCameraMove(transform.position, bird.transform.position));
    }

    void CameraMove(Vector3 dummy)
    {
        if(_coroutine != null) StopCoroutine(_coroutine);
        
        _coroutine = StartCoroutine(CameraMoveCoroutine());
    }
    
    IEnumerator CameraMoveCoroutine()
    {
        Vector3 pos = Vector3.zero;
        
        while (true)
        {
            pos = new Vector3(_bird.transform.position.x, _bird.transform.position.y, cam.transform.position.z);
            
            pos = ClampCamera(pos);
            
            transform.position = pos;
            
            yield return null;
        }
    }

    IEnumerator LerpCameraMove(Vector3 from, Vector3 to)
    {
        float percent = 0f;
        Vector3 pos = Vector3.zero;

        while (percent < movementTime)
        {
            percent += Time.deltaTime * speed;
            
            pos = Vector3.Lerp(from, to, percent / movementTime);

            pos = ClampCamera(pos);
            
            cam.transform.position = new Vector3(pos.x, pos.y, cam.transform.position.z);

            yield return null;
        }
    }

    Vector3 ClampCamera(Vector3 v)
    {
        return new Vector3(Mathf.Clamp(v.x,minX,maxX), Mathf.Clamp(v.y, minY, maxY), v.z);
    }
}