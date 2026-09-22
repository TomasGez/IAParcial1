using UnityEngine;
using UnityEngine.UI;

public class BaitUI : MonoBehaviour
{
    [SerializeField] private Image _disapearingBar;

    public void UpdateBaitHealth(float currentValue, float maxValue)
    {
        _disapearingBar.fillAmount = Mathf.Clamp(currentValue / maxValue, 0, 1);
    }
}
