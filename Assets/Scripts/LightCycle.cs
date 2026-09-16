using UnityEngine;

public class LightCycle : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(0f, Time.deltaTime, 0f, Space.World);
    }
}
