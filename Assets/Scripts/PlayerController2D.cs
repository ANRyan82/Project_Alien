using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    [SerializeField]
    public int lives = 3;
    [SerializeField]
    public float moveSpeed = 5f;      // Скорость перемещения по горизонтали
    [SerializeField]
    public float jumpForce = 7f;      // сила прыжка
    [SerializeField]
    public float crouchSpeedMultiplier = 0.5f; // Множитель при приседании


    new private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;
    private bool isGrounded;
    private bool isCrouching;
    private bool facingRight = true; // Направление взгляда

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Ввод по горизонтали
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Прыжок
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
        // Обработка приседания
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            isCrouching = true;
        }
        else
        {
            isCrouching = false;
        }
         // Расчет скорости с учетом приседания
        float currentSpeed = moveSpeed;
        if (isCrouching)
        {
            currentSpeed *= crouchSpeedMultiplier;
        }
        // Поворот персонажа
        if (moveInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && facingRight)
        {
            Flip();
        }
    }

    // Метод для разворота персонажа
    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1; // Инвертируем по горизонтали
        transform.localScale = scaler;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Проверка, касается ли персонаж земли
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
