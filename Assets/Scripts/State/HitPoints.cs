using AVA.Core;
using UnityEngine;

namespace AVA.State {
    public class HitPoints : ObservableValue<float>
    {
        public HitPoints(float initialValue) : base(initialValue)
        {
        }

        protected override void OnChanged()
        {
            Debug.Log("HitPoints changed");
            base.OnChanged();
        }
    }
}
