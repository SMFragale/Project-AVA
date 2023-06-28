using UnityEngine;
using UnityEngine.UI;
using AVA.Stats;
using System.Collections.Generic;

namespace AVA.UI.Stats {

    [RequireComponent(typeof(Image))]
    public class StatBar : MonoBehaviour
    {
        // This is temporary
        private Image bar;

        private StatType _type;

        private void Awake() {
            bar = GetComponent<Image>();
        }

        public void SetType(StatType type) {
            _type = type;
            bar.color = colors[type];
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
            {StatType.AttackSpeed, Color.yellow}
        };

        

        public void SetFillAmount(float fillAmount) {
            Debug.Log("StatBar: " + _type + " fill amount set from " + bar.fillAmount + " to " + fillAmount);
            bar.fillAmount = fillAmount;
        }
    }
}