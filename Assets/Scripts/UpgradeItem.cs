using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeItem : MonoBehaviour
{
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private UpgradeType _upgradeType;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _costText;
    public UpgradeType UpgradeType => _upgradeType;
    public Button Button => _upgradeButton;

    public void UpdateVisual(int upgradePrice, int upgradeLevel)
    {
        _costText.text = upgradePrice.ToString() + "$";
        _levelText.text = "LEVEL " + (upgradeLevel + 1).ToString();
    }
}