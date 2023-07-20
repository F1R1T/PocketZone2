using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterLogic : MonoBehaviour
{
    public float moveSpeed = 5f; // Скорость движения монстра
    public float attackRange = 1f; // Расстояние атаки монстра
    public float attackDamage = 10f; // Урон, который монстр наносит игроку
    public float sightRange = 10f; // Зона видимости монстра

    private bool isAttacking = false; // Флаг, указывающий на то, атакует ли монстр в данный момент
    private float timeSinceLastAttack = 0f; // Время с последней атаки
    private float attackDelay = 1f; // Задержка между атаками

    private Transform player; // Ссылка на трансформ игрока

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
       
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position); // Вычисляем расстояние до игрока

        if (distanceToPlayer <= sightRange) // Если игрок в зоне видимости монстра
        {
            if (!isAttacking || distanceToPlayer <= attackRange)
            {
                MoveTowardsPlayer(); // Если монстр не атакует или игрок находится в зоне атаки, то движется к игроку
            }

            if (isAttacking)
            {
                AttackPlayer(); // Если монстр атакует, то наносит урон игроку
            }
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector3 moveDirection = (player.position - transform.position).normalized; // Направление движения модели (от монстра к игроку)
        float distanceToPlayer = Vector3.Distance(transform.position, player.position); // Вычисляем расстояние до игрока

        if (distanceToPlayer > attackRange) // Если расстояние до игрока больше, чем дистанция атаки, то монстр движется к игроку
        {
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
            isAttacking = false; // Монстр больше не атакует, так как двигается к игроку
        }
        else // Иначе монстр начинает атаковать
        {
            isAttacking = true;
        }
    }

    private void AttackPlayer()
    {
        timeSinceLastAttack += Time.deltaTime; // Увеличиваем время с последней атаки

        float distanceToPlayer = Vector3.Distance(transform.position, player.position); // Вычисляем расстояние до игрока

        if (distanceToPlayer <= attackRange) // Если расстояние до игрока находится в пределах зоны атаки
        {
            if (timeSinceLastAttack >= attackDelay) // Если прошла задержка между атаками
            {
                player.GetComponent<Character>().ChangeHealth(-attackDamage);
                timeSinceLastAttack = 0f;
            }
        }
        else // Если расстояние до игрока превышает зону атаки
        {
            isAttacking = false; // Монстр больше не атакует, так как отошел от игрока
            timeSinceLastAttack = 0f;
        }
    }
    

}