using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{

    public Transform prefab;
    public int numberOfObjects;

    public float recycleOffset;
    public Vector3 startPosition;
    private Vector3 nextPosition;

    public Vector3 minSize, maxSize, minGap, maxGap;

    private Queue<Transform> objectQueue;

    public float minY, maxY;

    public Material[] materials;

    public PhysicMaterial[] physicMaterials;

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
        int materialIndex = Random.Range(0, materials.Length);
        o.GetComponent<Renderer>().material = materials[materialIndex];
        o.GetComponent<Collider>().material = physicMaterials[materialIndex];
		//nextPosition.x += scale.x;
		objectQueue.Enqueue(o);

        nextPosition += new Vector3(
            Random.Range (minGap.x, maxGap.x) + scale.x,
            Random.Range (minGap.y, maxGap.y),
            Random.Range (minGap.z, maxGap.z));

        if (nextPosition.y < minY) {
            nextPosition.y = minY + maxGap.y;
        }
        else if(nextPosition.y > maxY) {
            nextPosition.y = maxY - maxGap.y;
        }

    }
}
