using UnityEngine;
using UnityEngine.UI;

public class CircleAreaRenderer : MonoBehaviour
{
    public Image circleImage;
    public float radius = 5f;
    public Color color = Color.red;

    void Update()
    {
        circleImage.rectTransform.sizeDelta = new Vector2(radius*2, radius*2);
        circleImage.color = color;
    }
}
