using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkylineManager : MonoBehaviour
{

    public Transform prefab;
    public int numberOfObjects;

    public float recycleOffset;
    public Vector3 startPosition;
    private Vector3 nextPosition;

    public Vector3 minSize, maxSize;

    private Queue<Transform> objectQueue;


    // Start is called before the first frame update
    void Start()
    {
        objectQueue = new Queue<Transform> (numberOfObjects);

        for (int i = 0; i < numberOfObjects; i++){
            objectQueue.Enqueue((Transform)Instantiate(prefab));
        }

        nextPosition = startPosition;

        for (int i = 0; i < numberOfObjects; i++) {
            
                Recycle();
        }
    }




    // Update is called once per frame
    void Update()
    {
     if (objectQueue.Peek().localPosition.x + recycleOffset < Runner.distanceTraveled){
         Recycle();
     }   
    }

    private void Recycle () {
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
}
