using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image fill; // assign in Inspector

    private float previousHealth = -1f; // track previous health

    public void SetHealth(float current, float max)
    {
        fill.fillAmount = current / max;

        // show only when health decreases
        if (previousHealth < 0f)
            previousHealth = current;

        gameObject.SetActive(current < previousHealth);

        previousHealth = current;
    }
}
