using UnityEngine.UI;
using UnityEngine;
using System;

public class HunterUI : MonoBehaviour
{
    [SerializeField] private GameObject _patrolHuntUI;
    [SerializeField] private GameObject _baitUI;
    [SerializeField] private GameObject _gatherUI;
    [SerializeField] private Image _patrolIcon;
    [SerializeField] private Image _meleeIcon;
    [SerializeField] private Image _rangedIcon;
    [SerializeField] private Image _reloadBar;
    [SerializeField] private Image _placingBaitBar;
    [SerializeField] private Image _harvestIcon;

    public void ChangeHunterUI(Enum type)
    {
        switch (type)
        {
            case hunterModes.Patrol:
                _patrolHuntUI.SetActive(true);
                _baitUI.SetActive(false);
                _gatherUI.SetActive(false);
                _patrolIcon.enabled = true;
                _meleeIcon.enabled = false;
                _rangedIcon.enabled = false;
                break;
            case hunterModes.Bait:
                _patrolHuntUI.SetActive(false);
                _baitUI.SetActive(true);
                _gatherUI.SetActive(false);
                break;
            case hunterModes.Hunt:
                _patrolHuntUI.SetActive(true);
                _baitUI.SetActive(false);
                _gatherUI.SetActive(false);
                _patrolIcon.enabled = false;
                break;
            case hunterModes.Gather:
                _patrolHuntUI.SetActive(false);
                _baitUI.SetActive(false);
                _gatherUI.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void IsMeleeAttack(bool isClose)
    {
        if(isClose)
        {
            _meleeIcon.enabled = true;
            _rangedIcon.enabled = false;
        }
        else
        {
            _meleeIcon.enabled = false;
            _rangedIcon.enabled = true;
        }
    }

    public void ReloadAttack(float currentValue, float maxValue)
    {
        _reloadBar.fillAmount = Mathf.Clamp(currentValue / maxValue, 0, 1);
    }

    public void PlacingBait(float currentValue, float maxValue)
    {
        _placingBaitBar.fillAmount = Mathf.Clamp(currentValue / maxValue, 0, 1);
    }
}
