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

    public GameObject SpawnCircleArea(float radius)
    {
        var circleArea = Instantiate(circleAreaPrefab, transform);
        circleArea.GetComponent<CircleAreaRenderer>().radius = radius;

        return circleArea;
    }

    public void SpawnCircleArea(float radius, float duration)
    {
        var circleArea = SpawnCircleArea(radius);
        Destroy(circleArea, duration);
    }
}
