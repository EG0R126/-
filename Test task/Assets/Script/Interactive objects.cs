using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    public float interactionDistance = 3f; // Дистанция, с которой игрок может брать объекты
    public Transform handPosition; // Позиция руки, куда будут перемещаться объекты
    private GameObject pickedObject = null; // Объект, который игрок "взял"

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Нажатие кнопки "E" для взаимодействия
        {
            if (pickedObject == null)
            {
                TryPickUpObject();
            }
            else
            {
                DropObject();
            }
        }
    }

    void TryPickUpObject()
    {
        RaycastHit hit;

        // Используем луч для проверки, есть ли перед игроком объект для взаимодействия
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, interactionDistance))
        {
            if (hit.collider != null && hit.collider.CompareTag("Pickable")) // Проверяем, что объект имеет тег "Pickable"
            {
                pickedObject = hit.collider.gameObject;

                // Отключаем физику для объекта
                Rigidbody objectRb = pickedObject.GetComponent<Rigidbody>();
                if (objectRb != null)
                {
                    objectRb.isKinematic = true;
                }

                // Перемещаем объект в позицию руки и делаем его дочерним
                pickedObject.transform.SetParent(handPosition);
                pickedObject.transform.localPosition = Vector3.zero;
                pickedObject.transform.localRotation = Quaternion.identity;
            }
        }
    }

    void DropObject()
    {
        if (pickedObject != null)
        {
            // Включаем физику обратно
            Rigidbody objectRb = pickedObject.GetComponent<Rigidbody>();
            if (objectRb != null)
            {
                objectRb.isKinematic = false;
            }

            // Открепляем объект от руки
            pickedObject.transform.SetParent(null);
            pickedObject = null;
        }
    }
}