using UnityEngine;

public class PreyTrigger : MonoBehaviour
{
    private BoidAgent _father;

    private void Awake()
    {
        _father = GetComponentInParent<BoidAgent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Hunter"))
        {
            Debug.Log("Prey escaping from hunter");
            _father?.SetNPCTarget(other.GetComponent<NPCAgent>());
            _father.SetIsEscaping(true);
        }
    }
}
