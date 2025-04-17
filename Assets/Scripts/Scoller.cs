using System.Collections;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    [Header("Scroller Settings")]
    [SerializeField] private float initialSpeed = 7f;
    [SerializeField] private float speedIncreaseFactor = 0.1f;
    [SerializeField] private float maxSpeed = 15f;
    [SerializeField] private float speedIncreaseInterval = 20f;  // Intervalle en secondes pour augmenter la vitesse

    private float currentSpeed;

    void Start()
    {
        currentSpeed = initialSpeed;
        StartCoroutine(IncreaseSpeedOverTime());
    }

    IEnumerator IncreaseSpeedOverTime()
    {
        while (currentSpeed < maxSpeed)
        {
            yield return new WaitForSeconds(speedIncreaseInterval);
            currentSpeed += speedIncreaseFactor;
            currentSpeed = Mathf.Min(currentSpeed, maxSpeed);  // Limite la vitesse maximale
        }
    }

    void Update()
    {
        foreach (Transform child in transform)
            child.position += Vector3.back * currentSpeed * Time.deltaTime;
    }
}
