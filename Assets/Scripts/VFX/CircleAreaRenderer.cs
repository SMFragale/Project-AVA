using UnityEngine;
using UnityEngine.UI;

public class CircleAreaRenderer : MonoBehaviour
{
    public Image circleImage;
    public float radius = 5f;

    void Update()
    {
        circleImage.rectTransform.sizeDelta = new Vector2(radius*2, radius*2);
    }
}
