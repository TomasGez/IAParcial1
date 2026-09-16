using System;
using UnityEngine;

public class Bounds : MonoBehaviour
{
    public static Bounds Instance {get; private set;}

    [Header("Metrics")]
    [SerializeField] private float width;
    [SerializeField] private float height;
    [SerializeField] private bool drawBounds;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        if (!drawBounds)
        {
            return;
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(width, 0, height));
    }

    public Vector3 OutOfBounds(Vector3 position)
    {
        Vector3 newPosition = position;

        if(position.x > width / 2) newPosition.x -= width;
        if(position.x < -width / 2) newPosition.x += width;
        if(position.z > height / 2) newPosition.z -= height;
        if(position.z < -height / 2) newPosition.z += height;

        return newPosition;
    }
}
