using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderPositionAuto : MonoBehaviour
{
    #region 변수
    private Vector3 oDistance;
    private Transform targetTransform;
    private RectTransform oRectTransform;
    #endregion // 변수

    #region 함수
    // 플레이어 위치를 조정하고 실행되어야 해서 LateUpdate
    private void LateUpdate()
    {
        // 오브젝트의 월드 좌표를 기준으로 화면에서의 좌표 값을 구한다
        Vector3 ScreenPos = Camera.main.WorldToScreenPoint(targetTransform.position);
        // 화면내에서 좌표 + Distance만큼 떨어진 위치를 Slider UI의 위치로 설정
        oRectTransform.position = ScreenPos + oDistance;
    }

    public void Setup(Transform Target, Vector3 Distance)
    {
        // Slider UI 가 쫓아다닐 Target 설정
        targetTransform = Target;
        oDistance = Distance;
        oRectTransform = GetComponent<RectTransform>();
    }
    #endregion // 함수
}
