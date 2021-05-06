using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics;

[DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
public class NewBaseType
{
    public Transform prefab;
    public int numberOfObjects;
    public float recycleOffset;
    public Vector3 startPosition;

    public Vector3 minSize;

    public Vector3 maxSize;

    private Vector3 nextPosition;
    private Queue<Transform> objectQueue;

    private string GetDebuggerDisplay()
    {
        return ToString();
    }

    private void Recycle()
    {
        Vector3 scale = new Vector3(
            Random.Range(minSize.x, maxSize.x),
            Random.Range(minSize.y, maxSize.y),
            Random.Range(minSize.z, maxSize.z));

        Vector3 position = nextPosition;
        position.x += scale.x * 0.5f;
        position.y += scale.y * 0.5f;

        Transform o = objectQueue.Dequeue();
        o.localScale = scale;
        o.localPosition = position;
        nextPosition.x += scale.x;
        objectQueue.Enqueue(o);
    }

    void Start()
    {
        objectQueue = new Queue<Transform>(numberOfObjects);
        for (int i = 0; i < numberOfObjects; i++)
        {
            objectQueue.Enqueue((Transform)Instantiate(prefab));
        }
        nextPosition = startPosition;
        for (int i = 0; i < numberOfObjects; i++)
        {
            Recycle();
        }
    }

    void Update()
    {
        if (objectQueue.Peek().localPosition.x + recycleOffset < Runner.distanceTraveled)
        {
            Recycle();
        }
    }
}

[DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
public class SkylineManager : NewBaseType, MonoBehaviour {
}