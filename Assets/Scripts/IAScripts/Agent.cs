using UnityEngine;

public abstract class Agent : MonoBehaviour
{
    private Vector3 CurrentVelocity => currentVelocity;
    [HideInInspector] public Vector3 currentVelocity;
}
