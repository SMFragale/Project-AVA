using UnityEngine;
using UnityEngine.UI;
using AVA.Stats;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Rendering;
using System;

namespace AVA.UI.Stats {

    [RequireComponent(typeof(Image))]
    public class StatBar : MonoBehaviour
    {
        // This is temporary
        private Image bar;

        private StatType _type;

        [SerializeField]
        private TMP_Text statValue;

        [SerializeField]
        private Image maxBar;

        [SerializeField]
        private float minWidth = 100;

        private RectTransform rect;

        private void Awake() {
            bar = GetComponent<Image>();
            rect = GetComponent<RectTransform>();
        }

        public void SetType(StatType type) {
            _type = type;
            bar.color = colors[type];
            maxBar.color = bar.color;
            maxBar.color = new Color(maxBar.color.r, maxBar.color.g, maxBar.color.b, 0.3f);
        }

        public StatType GetStatType() {
            return _type;
        }

        // Dictionary initialized for the color of each stat
        private Dictionary<StatType, Color> colors = new Dictionary<StatType, Color> {
            {StatType.MaxHealth, Color.green},
            {StatType.Attack, Color.red},
            {StatType.Defense, Color.blue},
            {StatType.Speed, Color.gray},
            {StatType.AttackSpeed, Color.yellow},
            {StatType.CritChance, Color.magenta},
            {StatType.CritDamage, Color.cyan}
        };

        
        public void SetFillAmount(float currentValue, float maxValue) {
            bar.fillAmount = currentValue / maxValue;
            statValue.text = currentValue.ToString() + " / " + maxValue.ToString();

            // Set the width of the bar to be a value between minWidth and the width of the parent based on the maxValue / _type.maxValue
            rect.sizeDelta = new Vector2(Mathf.Max(minWidth, rect.parent.GetComponent<RectTransform>().rect.width * (maxValue / _type.maxValue)), rect.sizeDelta.y);
            //Set the size of the maxBar to be the same
            maxBar.rectTransform.sizeDelta = rect.sizeDelta;

            // Set the color of the maxBar to be a value between a lighter version of the color and a darker version of the color based on the maxValue / _type.maxValue
            bar.color = Color.Lerp(Color.Lerp(colors[_type], Color.white, 0.5f), Color.Lerp(colors[_type], Color.black, 0.5f), maxValue / _type.maxValue);
        }
    }
}