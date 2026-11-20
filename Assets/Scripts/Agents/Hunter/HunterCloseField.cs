using UnityEngine;

public class HunterCloseField : HunterTriggers
{
    private void OnTriggerEnter(Collider other)
    {
        if (!IsValidTarget(other.gameObject.layer, _agent.TargetMask) || _agent.IsResting) return;

        if (other.TryGetComponent<Animal>(out var animal))
        {
            if (animal == _agent.PreyTarget) _agent.StartHarvesting();
        }
    }
}
