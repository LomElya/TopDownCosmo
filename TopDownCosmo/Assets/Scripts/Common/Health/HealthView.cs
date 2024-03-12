using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [field: SerializeField] private Image _healthFill;

    public void ChangeFill(float value) => _healthFill.fillAmount = value;

    public void Remove() => Destroy(gameObject);
}
