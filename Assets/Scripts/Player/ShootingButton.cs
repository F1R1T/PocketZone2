using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingButton : MonoBehaviour
{
    [Header("Характеристики цели")]
    public List<GameObject> targets;
    private GameObject currentTarget;
    private int currentTargetIndex = -1;

    [Header("Характеристики урона")]
    public float damage = 10f;

    [Header("Зона видимости")]
    public float visibilityArea = 10f;
    public Transform visibilityCenter;

    // Вызывается при событии onClick кнопки выстрела
    public void Fire()
    {
        if (targets.Count == 0)
        {
            Debug.Log("Нет доступных целей!");
            return;
        }

        if (currentTargetIndex < 0 || currentTarget == null)
        {
            // Если цель еще не выбрана или текущая цель мертва, выбираем новую цель
            ChooseNextTarget();
        }

        if (currentTarget == null)
        {
            Debug.Log("Не осталось доступных целей!");
            return;
        }

        // Проверяем, находится ли текущая цель в зоне видимости
        if (!IsTargetInVisibilityArea(currentTarget))
        {
            Debug.Log("Цель не находится в зоне видимости!");
            return;
        }

        // Наносим урон текущей цели
        var targetHealth = currentTarget.GetComponent<Character>();
        if (targetHealth != null)
        {
            targetHealth.ChangeHealth(-damage);
        }

        if (targetHealth == null || targetHealth.currentHealth <= 0)
        {
            // Если цель умерла, выбираем новую цель
            ChooseNextTarget();
        }
    }

    // Вспомогательная функция для выбора следующей доступной цели
    private void ChooseNextTarget()
    {
        GameObject[] allMonsters = GameObject.FindGameObjectsWithTag("Monster");
        List<GameObject> availableTargets = new List<GameObject>();

        foreach (GameObject monster in allMonsters)
        {
            var monsterHealth = monster.GetComponent<Character>();
            if (monsterHealth != null && monsterHealth.currentHealth > 0 && IsTargetInVisibilityArea(monster))
            {
                availableTargets.Add(monster);
            }
        }

        if (availableTargets.Count == 0)
        {
            currentTarget = null;
            return;
        }

        currentTargetIndex = (currentTargetIndex + 1) % availableTargets.Count;
        currentTarget = availableTargets[currentTargetIndex];
    }

    // Проверяем, находится ли цель в зоне видимости
    private bool IsTargetInVisibilityArea(GameObject target)
    {
        if (visibilityCenter == null)
        {
            Debug.LogError("Центр зоны видимости не назначен!");
            return false;
        }

        float distance = Vector3.Distance(target.transform.position, visibilityCenter.position);
        return distance <= visibilityArea;
    }
}
