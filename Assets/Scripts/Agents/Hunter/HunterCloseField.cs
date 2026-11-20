using UnityEngine;

public class HunterCloseField : HunterTriggers
{
    private void OnTriggerStay(Collider other)
    {
        if (!IsValidTarget(other.gameObject.layer, _agent.TargetMask)) return;

        if (other.TryGetComponent<Animal>(out var animal))
        {
            if (animal == _agent.PreyTarget) _agent.InMeleeRange(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!IsValidTarget(other.gameObject.layer, _agent.TargetMask)) return;

        if (other.TryGetComponent<Animal>(out var animal))
        {
            if (animal == _agent.PreyTarget) _agent.InMeleeRange(false);
        }
    }
}
