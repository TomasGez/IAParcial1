using UnityEngine;

public class InterestItem : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float _currentHealth;

    private void Awake()
    {
        Manager.Instance.AddBait();
        _currentHealth = maxHealth;
    }

    private void Update()
    {
        if(_currentHealth <= 0)
        {
            Manager.Instance.RemoveBait();
            Destroy(gameObject);
        }
    }

    public void Interacting()
    {
        _currentHealth -= Time.deltaTime;
    }
}
