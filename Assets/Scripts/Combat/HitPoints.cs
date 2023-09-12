using AVA.Core;
using UnityEngine;

namespace AVA.Combat
{
    /// <summary>
    /// Base class for HitPoints. It is an <see cref="ObservableValue{T}">ObservableValue</see> of type float
    /// </summary>
    public class HitPoints : ObservableValue<float>
    {
        /// <summary>
        /// Constructor of a hitpoints object
        /// </summary>
        /// <param name="initialValue">The initial value of the hitpoints</param>
        public HitPoints(float initialValue) : base(initialValue)
        {
        }

        protected override void OnChanged()
        {
            base.OnChanged();
        }
    }
}
