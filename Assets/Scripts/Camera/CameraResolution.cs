using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    #region 변수
    private Camera oCamera;
    private Rect oRect;
    #endregion // 변수

    #region 함수
    private void Awake()
    {
        oCamera = GetComponent<Camera>();
        oRect = oCamera.rect;

        // 가로로 눕혀서 하는 게임 16 : 9
        float ScaleHeight = ((float)Screen.width / Screen.height) / ((float)16 / 9); // (가로 / 세로)
        float ScaleWidth = 1f / ScaleHeight;

        if (ScaleHeight < 1)
        {
            oRect.height = ScaleHeight;
            oRect.y = (1f - ScaleHeight) / 2f;
        }
        else
        {
            oRect.width = ScaleWidth;
            oRect.x = (1f - ScaleWidth) / 2f;
        }
        oCamera.rect = oRect;

    }
    #endregion // 함수

}
