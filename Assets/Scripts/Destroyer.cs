using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gelule"))
        {
            var gelule = other.gameObject.transform.parent;
            Destroy(gelule.gameObject);
        } else if (other.CompareTag("Chunk"))
        {
            var chunk = other.gameObject.transform.parent.parent;
            Destroy(chunk.gameObject);
        }        
    }
}
