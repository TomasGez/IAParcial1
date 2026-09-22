using UnityEngine;

public class BillboardCanvas : MonoBehaviour
{
    private Transform _camera;

    private void Awake()
    {
        if (_camera == null)
        {
            _camera = Camera.main.transform;
        }
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + _camera.forward);
    }
}