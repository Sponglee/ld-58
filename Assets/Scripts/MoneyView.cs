using TMPro;
using UnityEngine;

    public class MoneyView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        
        public void UpdateMoneyText(string text)
        {
            _text.text = text;
        }
    }
