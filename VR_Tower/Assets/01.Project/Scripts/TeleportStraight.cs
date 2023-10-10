using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportStraight : MonoBehaviour
{
    // 텔레포트를 표시할 UI
    public Transform teleportCircleUI = default;
    // 선을 그릴 라인 랜더러
    private LineRenderer lineRenderer = default;
    // 최초 텔레포트 UI의 크기
    private Vector3 originScale = Vector3.one * 0.02f;

    private void Awake()
    {
        // 시작할 때 비활성화한다.
        teleportCircleUI.gameObject.SetActive(false);

        // 라인 랜더러 컴포넌트 가져오기
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        // 왼쪽 컨트롤러의 One 버튼을 누르면
        if (ARAVRInput.GetDown(ARAVRInput.Button.One, ARAVRInput.Controller.LTouch))
        {
            // 라인 랜더러 컴포넌트 활성화
            lineRenderer.enabled = true;
        }
            
        // 왼쪽 컨트롤러의 One 버튼에서 손을 떼면
        else if (ARAVRInput.GetUp(ARAVRInput.Button.One, ARAVRInput.Controller.LTouch))
        {
            // 라인 랜더러 비활성화
            lineRenderer.enabled = false;
            if (teleportCircleUI.gameObject.activeSelf)
            {
                GetComponent<CharacterController>().enabled = false;
                // 텔레포트 UI 위치로 순간 이동
                transform.position = teleportCircleUI.position + Vector3.up;
                GetComponent<CharacterController>().enabled = true;
            }
            // 텔레포트 UI 비활성화
            teleportCircleUI.gameObject.SetActive(false);
        }

        //// 왼쪽 컨트롤러의 One 버튼을 누르고 있을 때
        //else if (ARAVRInput.Get(ARAVRInput.Button.One, ARAVRInput.Controller.LTouch))
        //{
          
        //}

        // 1. 왼쪽 컨트롤러를 기준으로 Ray를 만든다.
        Ray ray = new Ray(ARAVRInput.LHandPosition, ARAVRInput.LHandDirection);
        RaycastHit hitInfo = default;
        int layer = 1 << LayerMask.NameToLayer("Terrain");

        // 2. Terrain만 Ray 충돌 검출한다.
        if (Physics.Raycast(ray, out hitInfo, 200f, layer))
        {
            // 3. Ray가 부딪힌 지점에 라인 그리기
            lineRenderer.SetPosition(0, ray.origin);
            lineRenderer.SetPosition(1, hitInfo.point);

            // 4. Ray가 부딪힌 지점에 텔레포트 UI 표시
            teleportCircleUI.gameObject.SetActive(true);
            teleportCircleUI.position = hitInfo.point;
            // 텔레포트 UI가 위로 누워 있도록 방향을 설정한다.
            teleportCircleUI.forward = hitInfo.normal;
            // 텔레포트 UI의 크기가 거리에 따라 보정되도록 설정한다.
            teleportCircleUI.localScale = originScale * Mathf.Max(
                1f, hitInfo.distance);
        }
    }
} 
