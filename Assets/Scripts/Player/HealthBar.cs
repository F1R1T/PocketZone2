using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBarFilling;

    [SerializeField] Character health;

    private Camera cam;


    private void Awake()
    {
        health.HealthChanged += OnHealthChanged;
        cam = Camera.main;
        

    }

    private void OnDestroy()
    {
        health.HealthChanged -= OnHealthChanged;
    }

    public void OnHealthChanged(float valuesAsPercentage)
    {
        healthBarFilling.fillAmount = valuesAsPercentage;
    }

    private void LateUpdate()
    {
        transform.LookAt(new Vector3(transform.position.x, cam.transform.position.y, cam.transform.position.z));
        transform.Rotate(0, 180, 0);
    }
}


