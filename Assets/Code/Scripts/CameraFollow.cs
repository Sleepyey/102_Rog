using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 0, -10); // Z�� -10 ���� = ȭ���� ������� ���� ���� (2D�� ī�޶� �⺻�� Z = -10)
    public float speed = 4f;

    // LateUpdate ��� ���� -> Update���� �÷��̾� �̵��� ó�� �� �� ���� ī�޶� �־��ֱ� ����
    private void LateUpdate()   // LateUpdate = Update�� ���� �Ŀ� ȣ��
    {
        if (target == null)
        {
            return;
        }

        Vector3 cameraPosition = target.position + offset;
        Vector3 cameraspeedPosition = Vector3.Lerp(transform.position, cameraPosition, speed * Time.deltaTime);
        transform.position = cameraspeedPosition;   // �갡 �÷��̾ ���󰡰� ��
    }
}
