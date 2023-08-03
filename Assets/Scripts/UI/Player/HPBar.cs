using UnityEngine;
using UnityEngine.UI;
using AVA.Stats;
using TMPro;

namespace AVA.UI.Stats
{
    [RequireComponent(typeof(Image))]
    public class HPBar : MonoBehaviour
    {
        private StatType _type;

        [SerializeField]
        private TMP_Text statValue;

        [SerializeField]
        private Image maxBar;

        [SerializeField]
        private float minWidth = 100;

        private RectTransform rect;

        private void Awake()
        {
            rect = GetComponent<RectTransform>();
        }

        public void SetType(StatType type)
        {
            _type = type;
        }

        public StatType GetStatType()
        {
            return _type;
        }

        public void SetFillAmount(float currentValue, float maxValue)
        {
            maxBar.fillAmount = currentValue / maxValue;
            statValue.text = currentValue.ToString() + " / " + maxValue.ToString();

            // Set the width of the bar to be a value between minWidth and the width of the parent based on the maxValue / _type.maxValue
            rect.sizeDelta = new Vector2(Mathf.Max(minWidth, rect.parent.GetComponent<RectTransform>().rect.width * (maxValue / _type.maxValue)), rect.sizeDelta.y);
        }
    }
}