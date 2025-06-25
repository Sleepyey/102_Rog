using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 0, -10); // Z축 -10 고정 = 화면이 사라지는 버그 방지 (2D는 카메라 기본이 Z = -10)
    public float speed = 4f;

    // LateUpdate 사용 이유 -> Update에서 플레이어 이동을 처리 후 그 값을 카메라에 넣어주기 위해
    private void LateUpdate()   // LateUpdate = Update가 끝난 후에 호출
    {
        if (target == null)
        {
            return;
        }

        Vector3 cameraPosition = target.position + offset;
        Vector3 cameraspeedPosition = Vector3.Lerp(transform.position, cameraPosition, speed * Time.deltaTime);
        transform.position = cameraspeedPosition;   // 얘가 플레이어를 따라가게 됨
    }
}
