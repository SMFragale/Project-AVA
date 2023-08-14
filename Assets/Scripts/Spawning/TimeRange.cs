using UnityEngine;

[System.Serializable]
public class TimeRange
{
    [Range(0, 100)]
    [SerializeField]
    private float min;

    [Range(0, 100)]
    [SerializeField]
    private float max;

    public float Min { get => min; }
    public float Max { get => max; }

}
