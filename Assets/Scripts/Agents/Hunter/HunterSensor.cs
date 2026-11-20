using UnityEngine;

public class HunterSensor : HunterTriggers
{
    private void OnTriggerEnter(Collider other)
    {
        if (!IsValidTarget(other.gameObject.layer, _agent.TargetMask) || _agent.IsResting) return;

        if (other.TryGetComponent<Animal>(out var animal)) _agent.PreyEntered(animal);
    }
}
