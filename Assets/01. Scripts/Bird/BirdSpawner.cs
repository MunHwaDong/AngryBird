using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> bird;
    
    public delegate void OnBirdSpawned(Dragable newBird);
    public event OnBirdSpawned onBirdSpawned;

    void Start()
    {
        SpawnBird();
        EventBus.RegisterEvent(EventType.ENDTURN, SpawnBird);
    }

    void SpawnBird()
    {
        int rand = Random.Range(0, bird.Count);
        
        var newbird = Instantiate(bird[rand], transform.position, Quaternion.identity).GetComponent<Dragable>();
        
        onBirdSpawned?.Invoke(newbird);
    }
}
