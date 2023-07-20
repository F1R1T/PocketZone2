using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterLogic : MonoBehaviour
{
    public float moveSpeed = 5f; // �������� �������� �������
    public float attackRange = 1f; // ���������� ����� �������
    public float attackDamage = 10f; // ����, ������� ������ ������� ������
    public float sightRange = 10f; // ���� ��������� �������

    private bool isAttacking = false; // ����, ����������� �� ��, ������� �� ������ � ������ ������
    private float timeSinceLastAttack = 0f; // ����� � ��������� �����
    private float attackDelay = 1f; // �������� ����� �������

    private Transform player; // ������ �� ��������� ������

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
       
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position); // ��������� ���������� �� ������

        if (distanceToPlayer <= sightRange) // ���� ����� � ���� ��������� �������
        {
            if (!isAttacking || distanceToPlayer <= attackRange)
            {
                MoveTowardsPlayer(); // ���� ������ �� ������� ��� ����� ��������� � ���� �����, �� �������� � ������
            }

            if (isAttacking)
            {
                AttackPlayer(); // ���� ������ �������, �� ������� ���� ������
            }
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector3 moveDirection = (player.position - transform.position).normalized; // ����������� �������� ������ (�� ������� � ������)
        float distanceToPlayer = Vector3.Distance(transform.position, player.position); // ��������� ���������� �� ������

        if (distanceToPlayer > attackRange) // ���� ���������� �� ������ ������, ��� ��������� �����, �� ������ �������� � ������
        {
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
            isAttacking = false; // ������ ������ �� �������, ��� ��� ��������� � ������
        }
        else // ����� ������ �������� ���������
        {
            isAttacking = true;
        }
    }

    private void AttackPlayer()
    {
        timeSinceLastAttack += Time.deltaTime; // ����������� ����� � ��������� �����

        float distanceToPlayer = Vector3.Distance(transform.position, player.position); // ��������� ���������� �� ������

        if (distanceToPlayer <= attackRange) // ���� ���������� �� ������ ��������� � �������� ���� �����
        {
            if (timeSinceLastAttack >= attackDelay) // ���� ������ �������� ����� �������
            {
                player.GetComponent<Character>().ChangeHealth(-attackDamage);
                timeSinceLastAttack = 0f;
            }
        }
        else // ���� ���������� �� ������ ��������� ���� �����
        {
            isAttacking = false; // ������ ������ �� �������, ��� ��� ������ �� ������
            timeSinceLastAttack = 0f;
        }
    }
    

}