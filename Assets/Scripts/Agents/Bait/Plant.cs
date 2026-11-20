using UnityEngine;

public class Plant : MonoBehaviour
{
    private BaitUI _baitUI;

    private float _maxCondition;
    private float _currentCondition;

    protected void Awake()
    {
        _baitUI = GetComponentInChildren<BaitUI>();
    }

    public void InitializeBait(float condition)
    {
        _maxCondition = condition;
        _currentCondition = condition;
    }

    public void ConsumeBait()
    {
        _currentCondition -= Time.deltaTime;

        _baitUI.UpdateBottomBar(_currentCondition, _maxCondition);

        if (_currentCondition <= 0)
        {
            Destroy(gameObject);
        }
    }
}
