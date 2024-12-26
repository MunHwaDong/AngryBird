using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryQueue : MonoBehaviour
{
    private Queue<Trajectory> queue = new Queue<Trajectory>();
    
    private const int QueueSize = 150;

    public void Add(Trajectory trajectory)
    {
        if (queue.Count + 1 >= QueueSize)
        {
            Destroy(queue.Dequeue().gameObject);
        }
        
        queue.Enqueue(trajectory);
    }
}
