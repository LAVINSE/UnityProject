using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Define
{
    #region 기본
    // 기준이 되는 해상도
    public const float DESIGN_WIDTH = 1920.0f;
    public const float DESIGN_HEIGHT = 1080.0f;
    public const float CAMERA_FOV = 45.0f;

    public static readonly Vector3 DESIGN_SIZE = new Vector3(DESIGN_WIDTH, DESIGN_HEIGHT, 0.0f);
    #endregion // 기본

    #region 씬 이름
    public const string SCENE_STAGE = "SCENE_STAGE (스테이지)";
    #endregion // 씬 이름
}
