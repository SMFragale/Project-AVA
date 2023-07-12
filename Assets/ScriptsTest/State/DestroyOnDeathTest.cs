using AVA.Combat;
using UnityEngine;

[RequireComponent(typeof(HPService))]
public class DestroyOnDeathTest : MonoBehaviour
{
    private void Start()
    {
        GetComponent<HPService>().OnHealthZero.AddListener(() =>
        {
            Debug.Log(gameObject.name + " health has reached zero.");
            Destroy(gameObject);
        });
    }
}