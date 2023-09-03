using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CExtension
{
    #region 클래스 함수
    /** 작음 여부를 검사한다 */
    public static bool ExIsLess(this float NumberA, float NumberB)
    {
        return NumberA < NumberB - float.Epsilon;
    }

    /** 작거나 같음 여부를 검사한다 */
    public static bool ExIsLessEquals(this float NumberA, float NumberB)
    {
        return NumberA.ExIsLess(NumberB) || NumberA.ExIsEquals(NumberB);
    }

    /** 큰 여부를 검사한다 */
    public static bool ExIsGreat(this float NumberA, float NumberB)
    {
        return NumberA > NumberB + float.Epsilon;
    }

    /** 크거나 같음 여부를 검사한다 */
    public static bool ExIsGreatEquals(this float NumberA, float NumberB)
    {
        return NumberA.ExIsGreat(NumberB) || NumberA.ExIsEquals(NumberB);
    }

    /** 같음 여부를 검사한다 */
    public static bool ExIsEquals(this float NumberA, float NumberB)
    {
        return Mathf.Approximately(NumberA, NumberB);
    }
    #endregion // 클래스 함수
}

