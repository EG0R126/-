using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    public float interactionDistance = 3f; // ���������, � ������� ����� ����� ����� �������
    public Transform handPosition; // ������� ����, ���� ����� ������������ �������
    private GameObject pickedObject = null; // ������, ������� ����� "����"

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // ������� ������ "E" ��� ��������������
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

        // ���������� ��� ��� ��������, ���� �� ����� ������� ������ ��� ��������������
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, interactionDistance))
        {
            if (hit.collider != null && hit.collider.CompareTag("Pickable")) // ���������, ��� ������ ����� ��� "Pickable"
            {
                pickedObject = hit.collider.gameObject;

                // ��������� ������ ��� �������
                Rigidbody objectRb = pickedObject.GetComponent<Rigidbody>();
                if (objectRb != null)
                {
                    objectRb.isKinematic = true;
                }

                // ���������� ������ � ������� ���� � ������ ��� ��������
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
            // �������� ������ �������
            Rigidbody objectRb = pickedObject.GetComponent<Rigidbody>();
            if (objectRb != null)
            {
                objectRb.isKinematic = false;
            }

            // ���������� ������ �� ����
            pickedObject.transform.SetParent(null);
            pickedObject = null;
        }
    }
}