using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;             // Положение персонажа
    public float smoothSpeed = 0.3f;     // Скорость движения камеры
    public float distance = 5f;           // Расстояние между камерой и персонажем
    public float height = 5f;            // Высота камеры над персонажем

    private Vector3 offset;               // Смещение камеры от персонажа
    private Camera cam;                   // Ссылка на камеру

    void Start()
    {
        // Инициализация смещения камеры
        offset = new Vector3(0f, height, -distance);
        // Получение ссылки на камеру
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        // Получение позиции персонажа
        Vector3 targetPosition = target.position + offset;

        // Рассчет новой позиции камеры
        Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

        // Установка новой позиции камеры
        transform.position = newPosition;

        // Установка ориентации камеры
        transform.LookAt(target);
    }
}