using AVA.Core;
using UnityEngine;
using UnityEngine.UI;

namespace AVA.UI.Core {

    [RequireComponent(typeof(Image))]
    public abstract class ObservableValueBar<T> : MonoBehaviour
    {
        public ObservableValue<T> observableValue {get; private set;}
        public T maxValue {get; private set;}
        protected Image bar;

        private void Start() {
            bar = GetComponent<Image>();
        }

        public void SetObservableValue(ObservableValue<T> observableValue)
        {
            this.observableValue = observableValue;
            this.observableValue.AddOnChangedListener(UpdateBar);
        }

        public void SetMaxValue(T maxValue)
        {
            this.maxValue = maxValue;
        }

        protected abstract void UpdateBar();
    }

}