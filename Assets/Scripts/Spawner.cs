using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner Instance {get; private set;}

    private static List<Agent> _allAgents = new List<Agent>();
    private float _timer = 0f;

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
    }

    private void Update()
    {
        if(_allAgents.Count < maxAgents)
        {
            _timer += Time.deltaTime;

            if(_timer >= spawnTime)
            {
                _timer = 0;
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
