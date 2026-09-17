using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner Instance {get; private set;}

    private static List<Agent> _allAgents = new List<Agent>();
    private float _spawnerTimer;

    [Header("Spawner Data")]
    [SerializeField] private GameObject preyPrefab;
    [SerializeField] private float spawnTime;
    [SerializeField] private int maxAgents;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _spawnerTimer = spawnTime;
    }

    private void Update()
    {
        if(_allAgents.Count < maxAgents)
        {
            _spawnerTimer -= Time.deltaTime;

            if(_spawnerTimer <= 0)
            {
                _spawnerTimer = spawnTime;
                Instantiate(preyPrefab, transform.position, transform.rotation);
            }
        }
    }

    public List<Agent> GetAllAgents()
    {
        return _allAgents;
    }

    public void AddAgent(Agent agent)
    {
        _allAgents.Add(agent);
        Debug.Log("Prey spawned");
    }

    public void RemoveAgent(Agent agent)
    {
        _allAgents.Remove(agent);
        Debug.Log("Prey terminated");
    }
}
