using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> chunkPrefabs;
    [SerializeField] private Transform scrollerTransform;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Chunk"))
        {
            var chunk = other.gameObject.transform.parent.parent;
            var end = chunk.transform.Find("End");
            var newChunk = Instantiate(GetRandomChunk(), end.position, end.rotation);
            newChunk.transform.SetParent(scrollerTransform);
        }
    }

    private GameObject GetRandomChunk()
    {
        return chunkPrefabs[Random.Range(0, chunkPrefabs.Count)];
    }
}
