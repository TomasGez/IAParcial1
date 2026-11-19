using UnityEngine;
using UnityEngine.UI;

public class BaitUI : MonoBehaviour
{
    [SerializeField] private Image _conditionBackground;
    [SerializeField] private Image _conditionBar;
    [SerializeField] private Image _commonIcon;

    public void UpdateBottomBar(float currentValue, float maxValue)
    {
        float barFill = Mathf.Clamp(currentValue / maxValue, 0, 1);

        _conditionBar.fillAmount = barFill;
    }
}
