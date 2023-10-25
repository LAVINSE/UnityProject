using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveUI : Popup
{
    #region 변수
    [SerializeField] private GameObject ReTryButton;
    #endregion // 변수

    #region 함수
    /** 초기화 */
    private void Awake()
    {
        if(TurnManager.Instane.oIsPlayerDie == false)
        {
            ReTryButton.SetActive(false);
        }
    }

    /** 나가기 패널을 닫는다 */
    public void Close()
    {
        base.PopupClose();
    }

    /** 씬을 바꾼다 */
    public void ChangeScene(string SceneName)
    {
        AudioManager.Inst.PlaySFX(AudioManager.SFXEnum.OptionButton);
        LoadingScene.LoadScene(SceneName);
    }

    /** 나가기 패널을 생성한다 */
    public static LeaveUI CreateLeavePanel(GameObject RootObject)
    {
        var CreateLeavePanel = CFactory.CreateCloneObj<LeaveUI>("LeavePanel",
            Resources.Load<GameObject>("Prefabs/UiPrefabs/LeavePanel"), RootObject,
            Vector3.zero, Vector3.one, Vector3.zero);

        return CreateLeavePanel;
    }
    #endregion // 함수
}
