using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

public enum GeluleType
{
    Blue,
    Green,
    Red,  
    Yellow
}


public class GeluleController : MonoBehaviour
{
    [SerializeField]
    Vector3 rotationSpeed = new Vector3(100, 0, 100);

    public UnityEvent<float> OnChangeLife; // On envoie une valeur positive pour soigner
    public float value = 20f; // valeur de vie que la gélule rend

    public GeluleType type;

        
    private void Update()
    {
        transform.Find("Capsule").Rotate(rotationSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnChangeLife.Invoke(value);
            GameManager.Instance.PlayPickUpSound();
            Destroy(gameObject);
            GameManager.Instance.AddScore(10);
        }        
    }

 
}
