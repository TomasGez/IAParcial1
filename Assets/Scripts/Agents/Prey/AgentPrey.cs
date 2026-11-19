using System.Collections.Generic;
using UnityEngine;

public class AgentPrey : Agent
{
    [Header("Flock Data")]
    protected static List<Animal> _allPreys = new List<Animal>();
    [SerializeField] private float separationRadius;
    [SerializeField] private float alignmentRadius;
    [SerializeField] private float cohesionRadius;
    [SerializeField, Range(0f, 1f)] private float separationWeight;
    [SerializeField, Range(0f, 1f)] private float alignmentWeight;
    [SerializeField, Range(0f, 1f)] private float cohesionWeight;

}
