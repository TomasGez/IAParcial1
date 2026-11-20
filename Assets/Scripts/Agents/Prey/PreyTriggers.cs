using UnityEngine;

public class PreyTriggers : MonoBehaviour
{
    protected Animal _agent;

    private void Awake()
    {
        _agent = GetComponentInParent<Animal>();
    }

    protected static bool IsValidTarget(int layer, LayerMask mask) => (mask.value & (1 << layer)) != 0;
}
