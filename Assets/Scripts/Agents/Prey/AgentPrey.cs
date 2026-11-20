using UnityEngine;
using System.Collections.Generic;

public class AgentPrey : Agent
{
    [SerializeField] private float _keepTargetTime;

    private List<Animal> _nearbyPreys = new();
    private List<Plant> _allBaits = new();
    private Plant _baitTarget;
    private Archer _hunterTarget;

    public float KeepTargetTime => _keepTargetTime;
    public IReadOnlyList<Animal> NearbyPreys => _nearbyPreys;
    public Plant BaitTarget => _baitTarget;
    public Archer HunterTarget => _hunterTarget;

    public void PreyAttached(Animal target)
    {
        if (!_nearbyPreys.Contains(target)) _nearbyPreys.Add(target);
    }

    public void PreyDetached(Animal target)
    {
        if (_nearbyPreys.Contains(target)) _nearbyPreys.Remove(target);
    }

    public void ClearPreys(Animal agent)
    {
        foreach (Animal item in _nearbyPreys)
        {
            item.PreyDetached(agent);
        }

        if (_nearbyPreys.Count > 0) _nearbyPreys.Clear();
    }

    public void BaitEntered(Plant target)
    {
        if (!_allBaits.Contains(target)) _allBaits.Add(target);

        if (_baitTarget == null) _baitTarget = _allBaits[0];
    }

    public void BaitExited(Plant target)
    {
        if (_allBaits.Contains(target)) _allBaits.Remove(target);

        if (target == _baitTarget)
        {
            if (_allBaits.Count > 0) _baitTarget = _allBaits[0];

            else _baitTarget = null;
        }
    }

    public void ClearBaits()
    {
        if (_allBaits.Count > 0) _allBaits.Clear();

        if (_baitTarget != null) _baitTarget = null;
    }

    public void HunterEntered(Archer target)
    {
        if (_hunterTarget == null) _hunterTarget = target;
    }

    public void HunterExited(Archer target)
    {
        if (target == _hunterTarget) _hunterTarget = null;
    }
}
