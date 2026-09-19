using System;
using UnityEngine;
using UnityEngine.UI;

public class PreyUI : MonoBehaviour
{
    [SerializeField] private GameObject _flockUI;
    [SerializeField] private GameObject _examineUI;
    [SerializeField] private GameObject _escapeUI;
    [SerializeField] private GameObject _deadUI;
    [SerializeField] private Image _harvestBar;

    public void ChangePreyUI(Enum type)
    {
        _flockUI.SetActive(false);
        _examineUI.SetActive(false);
        _escapeUI.SetActive(false);
        _deadUI.SetActive(false);

        switch (type)
        {
            case preyModes.Flock:
                _flockUI.SetActive(true);
                break;
            case preyModes.Examine:
                _examineUI.SetActive(true);
                break;
            case preyModes.Escape:
                _escapeUI.SetActive(true);
                break;
            case preyModes.Dead:
                _deadUI.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void BeingHarvested(float currentValue, float maxValue)
    {
        _harvestBar.fillAmount = Mathf.Clamp(currentValue / maxValue, 0, 1);
    }
}
