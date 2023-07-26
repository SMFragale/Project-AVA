using AVA.Core;
using UnityEngine;

namespace AVA.Combat
{
    public class HitPoints : ObservableValue<float>
    {
        public HitPoints(float initialValue) : base(initialValue)
        {
        }

        protected override void OnChanged()
        {
            base.OnChanged();
        }
    }
}
