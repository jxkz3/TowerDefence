using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image fill; // assign in Inspector

    public void SetHealth(float current, float max)
    {
        fill.fillAmount = current / max;
    }
}
