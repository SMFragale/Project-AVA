using System.Collections;
using AVA.Combat;
using UnityEngine;

public class CharacterColorChange : MonoBehaviour
{
    [SerializeField]
    private CombatTarget combatTarget;

    [SerializeField]
    private Color damageColor = Color.red;

    [SerializeField]
    private float colorChangeDuration = 0.5f;

    private Renderer characterRenderer;
    private Color originalColor;

    private void Start()
    {
        // Assuming your character has a Renderer component.
        characterRenderer = GetComponent<Renderer>();
        originalColor = characterRenderer.material.color;
        combatTarget.OnTakeDamage.AddListener(TakeDamage);
        Debug.Log("Listener set");
    }

    // Method to trigger the color change externally.
    public void TakeDamage(float damage)
    {
        StartCoroutine(ChangeColorOnDamage());
    }

    private IEnumerator ChangeColorOnDamage()
    {
        characterRenderer.material.color = damageColor;
        yield return new WaitForSeconds(colorChangeDuration);
        characterRenderer.material.color = originalColor;
    }
}
