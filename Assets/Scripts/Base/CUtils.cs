using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[System.Serializable]
public class PRS
{
    public Vector3 Position;
    public Vector3 Scale;
    public Quaternion Rotation;

    public PRS(Vector3 Position, Vector3 Scale, Quaternion Rotation)
    {
        this.Position = Position;
        this.Scale = Scale;
        this.Rotation = Rotation;
    }
}

public static class CUtils
{
    public static Vector3 MousePosition
    {
        get
        {
            // 스크린에서 월드 포인트로 변환
            Vector3 Result = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // 카메라가 -100이기 때문에 결과를 -10으로 설정
            Result.z = -10;
            return Result;
        }
    }
}
