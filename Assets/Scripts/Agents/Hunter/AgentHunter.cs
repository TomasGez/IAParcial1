using UnityEngine;
using System.Collections.Generic;

public class AgentHunter : Agent
{
    private List<Animal> _allPreys = new();
    private Animal _preyTarget;

    public Animal PreyTarget => _preyTarget;

    public void PreyEntered(Animal target)
    {
        if (!_allPreys.Contains(target)) _allPreys.Add(target);

        if (_preyTarget == null) _preyTarget = _allPreys[0];
    }

    public void PreyExited(Animal target)
    {
        if (target == _preyTarget)
        {
            if (_allPreys.Count > 0)
            {
                _preyTarget = _allPreys[0];
            }
            else
            {
                _preyTarget = null;
            }
        }
    }
}
