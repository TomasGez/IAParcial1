using UnityEngine;

public enum HunterMode { Patrol, Bait, Hunt, Harvest, Rest }
public enum PreyMode { Flock, Examine, Consume, Escape, Dead, Respawn }

public class Manager : MonoBehaviour
{
    public static Manager Instance {get; private set;}

    private static int _baitAmount;

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

    public int GetBaitAmount()
    {
        return _baitAmount;
    }

    public void AddBait()
    {
        _baitAmount += 1;
    }

    public void RemoveBait()
    {
        _baitAmount -= 1;
    }
}
