using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region 변수
    [SerializeField] private GameObject ScalingTargetObj = null;

    private Camera oCamera = null;
    #endregion // 변수


    #region 함수
    /** 초기화 */
    private void Awake()
    {
        oCamera = this.GetComponent<Camera>();
    }

    /** 초기화 */
    private void Start()
    {
        this.SetupScalingTarget();
        this.SetupCamera();
    }
    /** 카메라를 설정한다 */
    private void SetupCamera()
    {
        float Fov = Define.CAMERA_FOV;
        float Height = Define.DESIGN_HEIGHT;

        float Distance = (Height / 2.0f) / Mathf.Tan((Fov / 2.0f) * Mathf.Deg2Rad);

        oCamera.transform.localPosition = new Vector3(0.0f, 0.0f, -Distance);

        this.Setup2DCamera();
    }

    /** 2D 카메라를 설정한다 */
    private void Setup2DCamera()
    {
        oCamera.orthographic = true;

        // 기준 해상도 절반
        oCamera.orthographicSize = Define.DESIGN_HEIGHT / 2.0f;
    }

    /** 3D 카메라를 설정한다 */
    private void Setup3DCamera()
    {
        oCamera.fieldOfView = Define.CAMERA_FOV;
        oCamera.orthographic = false;
    }

    /** 비율 대상을 설정한다 */
    private void SetupScalingTarget()
    {
        var Scale = Vector3.one * CAccess.GetResolutionScale(Define.DESIGN_SIZE);
        ScalingTargetObj.transform.localScale = Scale;
    }
    #endregion // 함수
}
