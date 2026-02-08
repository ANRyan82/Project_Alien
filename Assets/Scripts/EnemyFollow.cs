using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform player;             // Трансформ персонажа
    public float speed = 2f;             // Скорость движения врага
    public float sightRange = 10f;       // Дальность видимости
    public Transform eyePoint;           // Точка, откуда идёт проверка зрения (можно разместить на голове врага)

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Проверяем, видит ли враг персонажа
        Vector2 directionToPlayer = player.position - eyePoint.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        if (distanceToPlayer <= sightRange)
        {
            RaycastHit2D hit = Physics2D.Raycast(eyePoint.position, directionToPlayer.normalized, sightRange);
            if (hit.collider != null && hit.collider.gameObject.CompareTag("Player"))
            {
                // Враг видит игрока, движемся к нему
                MoveTowardsPlayer(directionToPlayer.normalized);
            }
            else
            {
                // Не видит, останавливается или патрулирует
                rb.velocity = Vector2.zero;
            }
        }
        else
        {
            // За пределами видимости
            rb.velocity = Vector2.zero;
        }
    }

    void MoveTowardsPlayer(Vector2 direction)
    {
        rb.velocity = new Vector2(Mathf.Sign(direction.x) * speed, rb.velocity.y);
        // Можно дополнительно менять направление спрайта или анимацию
    }
}