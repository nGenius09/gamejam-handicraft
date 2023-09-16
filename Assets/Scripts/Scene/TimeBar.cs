using UnityEngine;

public class TimeBar : MonoBehaviour
{
    [SerializeField] private SpriteRenderer gauge;
    [SerializeField] private float min;
    [SerializeField] private float max;

    public void SetFillAmount(float rate)
    {
        var size = gauge.size;
        size.x = Mathf.Clamp(rate, min, max);
        gauge.size = size;
    }
}