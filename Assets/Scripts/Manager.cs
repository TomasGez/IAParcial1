using UnityEngine;

public enum HunterMode { Patrol, Bait, Hunt, Harvest, Rest }
public enum PreyMode { Flock, Examine, Consume, Escape, Dead, Respawn }

public class Manager : MonoBehaviour
{
    public static Manager Instance {get; private set;}

    private static int _plantsInScene;

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

    public int GetTotalPlants()
    {
        return _plantsInScene;
    }

    public void AddPlant()
    {
        _plantsInScene += 1;
    }

    public void RemovePlant()
    {
        _plantsInScene -= 1;
    }
}
