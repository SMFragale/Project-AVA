using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class CharacterVFXController : MonoBehaviour
{
    public Canvas canvas => GetComponent<Canvas>();

    public GameObject circleAreaPrefab;

    public void SpawnCircleAreaDebug(float radius)
    {
        SpawnCircleArea(radius);
    }

    public GameObject SpawnCircleArea(float radius, Color color = default)
    {
        
        var circleArea = Instantiate(circleAreaPrefab, transform);
        circleArea.GetComponent<CircleAreaRenderer>().radius = radius;
        if (color != default)
        {
            circleArea.GetComponent<CircleAreaRenderer>().color = color;
        }

        return circleArea;
    }

    public void SpawnCircleArea(float radius, float duration, Color color = default)
    {
        var circleArea = SpawnCircleArea(radius, color);
        Destroy(circleArea, duration);
    }
}
