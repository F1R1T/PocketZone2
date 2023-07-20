using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Health stats")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] public float currentHealth;
    public event Action<float> HealthChanged;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {

    }

    public void ChangeHealth(float valueAmount)
    {
        currentHealth += valueAmount;

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            float currentHealthAsPercantage = (float)currentHealth / maxHealth;
            HealthChanged?.Invoke(currentHealthAsPercantage);
        }
    }

    private void Die()
    {
        if (gameObject.CompareTag("Player"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
        else if (gameObject.CompareTag("Monster"))
        {
            ItemDropper dropper = GetComponent<ItemDropper>();
            if (dropper != null)
            {
                dropper.DropItem();
            }
            Destroy(gameObject);
        }
    }
}