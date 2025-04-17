using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof(Rigidbody))]
[RequireComponent (typeof(PlayerInputs))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed = 10f;

    private Rigidbody rb;
    private PlayerInputs playerInputs;

    private float life = 100f;

    public UnityEvent<float> OnInitLife;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerInputs = GetComponent<PlayerInputs>();
        OnInitLife.Invoke(life);
    }

    private void FixedUpdate()
    {
        HandleMovements();
    }

    private void HandleMovements()
    {
        Vector3 targetPosition = transform.position + (Vector3.right * playerInputs.lateralInput * movementSpeed * Time.fixedDeltaTime);
        rb.MovePosition(targetPosition);
    }

}
