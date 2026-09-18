using UnityEngine;

public class InterestItem : MonoBehaviour
{
    private float _destroyTimer = 0;
    [SerializeField] private float destroyCooldown;

    public void Interacting()
    {
        _destroyTimer += Time.deltaTime;

        if(_destroyTimer > destroyCooldown)
        {
            Manager.Instance.RemoveBait();
            Destroy(this);
        }
    }
}
