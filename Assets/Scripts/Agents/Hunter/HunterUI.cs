using UnityEngine;
using UnityEngine.UI;

public class HunterUI : MonoBehaviour
{
    [SerializeField] private Image _energyBackground;
    [SerializeField] private Image _energyBar;
    [SerializeField] private Image _patrolIcon;
    [SerializeField] private Image _baitIcon;
    [SerializeField] private Image _placeBackground;
    [SerializeField] private Image _placeBar;
    [SerializeField] private Image _huntIcon;
    [SerializeField] private Sprite _farTexture;
    [SerializeField] private Sprite _rangedTexture;
    [SerializeField] private Sprite _meleeTexture;
    [SerializeField] private Image _attackBackground;
    [SerializeField] private Image _attackBar;
    [SerializeField] private Image _harvestIcon;
    [SerializeField] private Image _gatherBackground;
    [SerializeField] private Image _gatherBar;
    [SerializeField] private Image _restIcon;

    public Sprite FarTexture => _farTexture;
    public Sprite RangedTexture => _rangedTexture;
    public Sprite MeleeTexture => _meleeTexture;

    public void ChangeMode(HunterMode mode)
    {
        _patrolIcon.enabled = false;
        _baitIcon.enabled = false;
        _placeBackground.enabled = false;
        _placeBar.enabled = false;
        _huntIcon.enabled = false;
        _attackBackground.enabled = false;
        _attackBar.enabled = false;
        _harvestIcon.enabled = false;
        _gatherBackground.enabled = false;
        _gatherBar.enabled = false;
        _restIcon.enabled = false;

        switch (mode)
        {
            case HunterMode.Patrol:
                _patrolIcon.enabled = true;
                break;
            case HunterMode.Bait:
                _baitIcon.enabled = true;
                _placeBackground.enabled = true;
                _placeBar.enabled = true;
                break;
            case HunterMode.Hunt:
                _huntIcon.enabled = true;
                _attackBackground.enabled = true;
                _attackBar.enabled = true;
                break;
            case HunterMode.Harvest:
                _harvestIcon.enabled = true;
                _gatherBackground.enabled = true;
                _gatherBar.enabled = true;
                break;
            case HunterMode.Rest:
                _restIcon.enabled = true;
                break;
            default:
                break;
        }
    }

    public void ChangeHuntIcon(Sprite texture)
    {
        _huntIcon.sprite = texture;
    }

    public void UpdateBar(float currentValue, float maxValue, HunterMode mode)
    {
        float barFill = Mathf.Clamp(currentValue / maxValue, 0, 1);

        switch (mode)
        {
            case HunterMode.Bait:
                _placeBar.fillAmount = barFill;
                break;
            case HunterMode.Hunt:
                _attackBar.fillAmount = barFill;
                break;
            case HunterMode.Harvest:
                _gatherBar.fillAmount = barFill;
                break;
            default:
                break;
        }
    }

    public void UpdateBottomBar(float currentValue, float maxValue)
    {
        float barFill = Mathf.Clamp(currentValue / maxValue, 0, 1);

        _energyBar.fillAmount = barFill;
    }
}
