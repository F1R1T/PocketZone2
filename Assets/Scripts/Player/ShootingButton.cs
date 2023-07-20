using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingButton : MonoBehaviour
{
    [Header("�������������� ����")]
    public List<GameObject> targets;
    private GameObject currentTarget;
    private int currentTargetIndex = -1;

    [Header("�������������� �����")]
    public float damage = 10f;

    [Header("���� ���������")]
    public float visibilityArea = 10f;
    public Transform visibilityCenter;

    // ���������� ��� ������� onClick ������ ��������
    public void Fire()
    {
        if (targets.Count == 0)
        {
            Debug.Log("��� ��������� �����!");
            return;
        }

        if (currentTargetIndex < 0 || currentTarget == null)
        {
            // ���� ���� ��� �� ������� ��� ������� ���� ������, �������� ����� ����
            ChooseNextTarget();
        }

        if (currentTarget == null)
        {
            Debug.Log("�� �������� ��������� �����!");
            return;
        }

        // ���������, ��������� �� ������� ���� � ���� ���������
        if (!IsTargetInVisibilityArea(currentTarget))
        {
            Debug.Log("���� �� ��������� � ���� ���������!");
            return;
        }

        // ������� ���� ������� ����
        var targetHealth = currentTarget.GetComponent<Character>();
        if (targetHealth != null)
        {
            targetHealth.ChangeHealth(-damage);
        }

        if (targetHealth == null || targetHealth.currentHealth <= 0)
        {
            // ���� ���� ������, �������� ����� ����
            ChooseNextTarget();
        }
    }

    // ��������������� ������� ��� ������ ��������� ��������� ����
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

    // ���������, ��������� �� ���� � ���� ���������
    private bool IsTargetInVisibilityArea(GameObject target)
    {
        if (visibilityCenter == null)
        {
            Debug.LogError("����� ���� ��������� �� ��������!");
            return false;
        }

        float distance = Vector3.Distance(target.transform.position, visibilityCenter.position);
        return distance <= visibilityArea;
    }
}
