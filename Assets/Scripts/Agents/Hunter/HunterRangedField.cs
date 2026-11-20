using UnityEngine;

public class HunterRangedField : HunterTriggers
{
    private void OnTriggerStay(Collider other)
    {
        if (!IsValidTarget(other.gameObject.layer, _agent.TargetMask)) return;

        if (other.TryGetComponent<Animal>(out var animal))
        {
            if (animal == _agent.PreyTarget) _agent.InShootRange(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!IsValidTarget(other.gameObject.layer, _agent.TargetMask)) return;

        if (other.TryGetComponent<Animal>(out var animal))
        {
            if (animal == _agent.PreyTarget) _agent.InShootRange(false);
        }
    }
}
