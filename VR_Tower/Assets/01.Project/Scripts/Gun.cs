using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // { 총알 관련 변수
    public Transform bulletImpact = default;
    private ParticleSystem bulletEffect = default;
    private AudioSource bulletAudio = default;
    // } 총알 관련 변수

    // 조준점 관련 변수
    public Transform crosshair = default;

    // Start is called before the first frame update
    void Start()
    {
        // 총알 FX 파티클 시스템 컴포넌트 가져오기
        bulletEffect = bulletImpact.GetComponent<ParticleSystem>();
        // 총알 효과 오디오 소스 컴포넌트 가져오기
        bulletAudio = bulletImpact.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        // 크로스 헤어 표시
        ARAVRInput.DrawCrosshair(crosshair);
        // 사용자가 IndexTrigger 버튼을 누르면
        if (ARAVRInput.GetDown(ARAVRInput.Button.IndexTrigger))
        {
            // 총알 오디오 재생
            bulletAudio.Stop();
            bulletAudio.Play();

            // Ray를 카메라의 위치로부터 나가도록 만든다.
            Ray ray = new Ray(ARAVRInput.RHandPosition, ARAVRInput.RHandDirection);
            // Ray의 충돌 정보를 저장하기 위한 변수를 지정한다.
            RaycastHit hitInfo = default;
            // 플레이어 레이어 얻어오기
            int PlayerLayer = 1 << LayerMask.NameToLayer("Player");
            // 타워 레이어 얻어오기
            int towerLayer = 1 << LayerMask.NameToLayer("Tower");
            int layerMask = PlayerLayer | towerLayer;

            // Ray를 쏜다. ray가 부딪힌 정보는 hitInfo에 저장한다.
            // ~layerMask는 설정한 layerMask이외의 모든것
            // ~(NOT) 연산자는 비트 연산자 전용이다. 논리는 !(NOT) 사용
            // 만약 !를 사용할 경우 PlayerLayer | towerLayer만 처리한다. 
            if (Physics.Raycast(ray, out hitInfo, 200f, ~layerMask))
            {
                // 총알 이펙트가 진행되고 있으면 멈추고 재생
                bulletEffect.Stop();
                bulletEffect.Play();
                // 부딪힌 지점 바로 위에서 이펙트가 보이도록 설정한다.
                bulletImpact.position = hitInfo.point;
                // 부딪힌 지점의 방향으로 총알 이펙트의 방향을 설정한다.
                bulletImpact.forward = hitInfo.normal;
                
            }
        }
    }       // Update()
}       // Class Gun
