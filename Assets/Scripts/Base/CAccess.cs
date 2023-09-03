using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CAccess
{
    #region 클래스 프로퍼티
    public static float ScreenWidth
    {
        get
        {
            // 유니티 에디터 일때 픽셀 반환, 아닐때 스크린 반환
            // 스크린 클래스를 활용하면 현재 디바이스의 너비와 높이를 가져오는것이 가능
#if UNITY_EDITOR
            return Camera.main.pixelWidth;
#else
            return Screen.width;
#endif // #if UNITY_EDITOR
        }
    }

    public static float ScreenHeight
    {
        get
        {
#if UNITY_EDITOR
            return Camera.main.pixelHeight;
#else
            return Screen.height;
#endif // #if UNITY_EDITOR
        }
    }

    public static Vector3 ScreenSize => new Vector3(ScreenWidth, ScreenHeight, 0.0f);
    #endregion // 클래스 프로퍼티

    
    #region 클래스 함수
    /** 해상도 비율을 반환한다 */
    public static float GetResolutionScale(Vector3 DesignSize)
    {
        float Aspect = DesignSize.x / DesignSize.y;
        float oScreenWidth = ScreenHeight * Aspect;

        // 종횡비로 구한 width가 현재 디바이스 width보다 클때 scale , 작거나 같으면 scale X
        return oScreenWidth.ExIsLessEquals(ScreenWidth) ? 1.0f : ScreenWidth / oScreenWidth;
    }
    #endregion // 클래스 함수
}
