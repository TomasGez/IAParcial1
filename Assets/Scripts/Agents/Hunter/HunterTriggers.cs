using UnityEngine;

public class HunterTriggers : MonoBehaviour
{
    protected Archer _agent;

    private void Awake()
    {
        _agent = GetComponentInParent<Archer>();
    }

    protected static bool IsValidTarget(int layer, LayerMask mask) => (mask.value & (1 << layer)) != 0;
}
