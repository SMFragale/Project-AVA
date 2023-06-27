using UnityEngine.Events;

namespace AVA.Core
{
    [System.Serializable]
    public class ObservableValue<T>
    {
        public UnityEvent OnValueChanged;

        private T val;
        public T Value
        {
            get { return val; }
            set
            {
                if (!value.Equals(val))
                {
                    val = value;
                    OnChanged();
                }
            }
        }

        public ObservableValue(T initialValue)
        {
            Value = initialValue;
            OnValueChanged = new UnityEvent();
        }

        protected virtual void OnChanged()
        {
            OnValueChanged?.Invoke();
        }

        protected void OnDestroy()
        {
            RemoveAllListeners();
        }

        protected void OnDisable()
        {
            RemoveAllListeners();
        }


        public void AddOnChangedListener(UnityAction action)
        {
            OnValueChanged.AddListener(action);
        }

        public void RemoveOnChangedListener(UnityAction action)
        {
            OnValueChanged.RemoveListener(action);
        }

        public void RemoveAllListeners()
        {
            OnValueChanged.RemoveAllListeners();
        }
    }
}
