using UnityEngine;

public class BoundsTrigger : MonoBehaviour
{
    [Header("Bounds")]
    [SerializeField] private BoxCollider _collider;

    private void Wraparound(Agent target)
    {
        Vector3 localPos = transform.InverseTransformPoint(target.transform.position);

        if (localPos.x > _collider.size.x / 2) localPos.x -= _collider.size.x;
        if (localPos.x < -_collider.size.x / 2) localPos.x += _collider.size.x;
        if (localPos.z > _collider.size.z / 2) localPos.z -= _collider.size.z;
        if (localPos.z < -_collider.size.z / 2) localPos.z += _collider.size.z;

        target.transform.position = transform.TransformPoint(localPos);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Animal>(out var animal))
        {
            Wraparound(animal);
        }
    }
}
