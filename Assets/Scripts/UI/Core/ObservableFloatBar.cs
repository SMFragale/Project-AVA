namespace AVA.UI.Core
{
    public class ObservableFloatBar : ObservableValueBar<float>
    {
        protected override void UpdateBar()
        {
            if (observableValue != null)
            {
                bar.fillAmount = observableValue.Value / maxValue;
            }
        }
    }

}

