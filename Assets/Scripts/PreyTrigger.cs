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

        if (other.gameObject.layer == LayerMask.NameToLayer("Bait") && _father.GetCurrentItem() == null)
        {
            Debug.Log("Prey examining bait");
            _father?.SetCurrentItem(other.GetComponent<InterestItem>());
            _father.SetIsExamining(true);
        }
    }
}
