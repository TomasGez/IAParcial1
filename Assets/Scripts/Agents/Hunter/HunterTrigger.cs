using UnityEngine;

public class HunterTrigger : MonoBehaviour
{
    private NPCAgent _father;

    private void Awake()
    {
        _father = GetComponentInParent<NPCAgent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Prey"))
        {
            Debug.Log("Hunter following new prey");
            _father.AddBoidTarget(other.GetComponent<BoidAgent>());
            _father.SetIsHunting(true);
        }
    }
}
