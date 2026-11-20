using UnityEngine;

public class PreyCloseField : PreyTriggers
{
    private void OnTriggerEnter(Collider other)
    {
        if (!IsValidTarget(other.gameObject.layer, _agent.TargetMask) || _agent.IsDead) return;

        if (other.TryGetComponent<Plant>(out var plant))
        {
            if (plant == _agent.BaitTarget) _agent.StartConsuming();
        }
    }
}
