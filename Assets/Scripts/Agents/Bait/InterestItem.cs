using UnityEngine;

public class InterestItem : MonoBehaviour
{
    [SerializeField] private BaitUI baitUI;
    [SerializeField] private float maxHealth;
    private float _currentHealth;

    private void Awake()
    {
        Manager.Instance.AddBait();
        _currentHealth = maxHealth;
    }

    public void Interacting()
    {
        if (_currentHealth <= 0)
        {
            Manager.Instance.RemoveBait();
            Destroy(gameObject);
        }

        _currentHealth -= Time.deltaTime;
        baitUI.UpdateBaitHealth(_currentHealth, maxHealth);
    }
}
