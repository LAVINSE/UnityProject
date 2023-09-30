using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct StageInfo
{
    public enum EnemyType
    {
        // NONE 사용 X
        NONE = -1,

        NORMAL,
        ELITE,
        BOSS,
    }

    public string StageInfoTitleName;
    public Sprite StageInfoImg;
    public EnemyType StageEnemyType;
}

public class StageMenuUI : MonoBehaviour
{
    #region 변수
    [Header("=====> Stage Info <=====")]
    [SerializeField] private StageInfo[] StageInfoArray = null;

    [Header("=====> Stage UI <=====")]
    [SerializeField] private TMP_Text StageTitleText = null;
    [SerializeField] private Image StageImg = null;

    private int CurrentStage = 0;
    #endregion // 변수

    #region 함수
    /** 초기화 */
    private void Start()
    {
        SettingStageInfo();
    }

    /** Next 스테이지 버튼을 눌렀을 때*/
    public void OnClickNextButton()
    {
        if(++CurrentStage > StageInfoArray.Length - 1)
        {
            CurrentStage = 0;
        }
        SettingStageInfo();
    }

    /** Back 스테이지 버튼을 눌렀을 때*/
    public void OnClickPriorButton()
    {
        if (--CurrentStage < 0)
        {
            CurrentStage = StageInfoArray.Length - 1;
        }
        SettingStageInfo();
    }

    /** 스테이지 정보를 세팅한다 */
    private void SettingStageInfo()
    {
        StageTitleText.text = StageInfoArray[CurrentStage].StageInfoTitleName;
        StageImg.sprite = StageInfoArray[CurrentStage].StageInfoImg;
    }

    /** 플레이 버튼을 눌렀을 때 */
    public void OnClickPlayButton()
    {
        GameManager.Instance.ActiveStageUI("StageUI_0");
        GameManager.Instance.ActiveStageObject("StageObject_0");
        GameManager.Instance.oStageEnemyType = StageInfoArray[CurrentStage].StageEnemyType;
        this.gameObject.SetActive(false);
        GameManager.Instance.IsGameStart = true;
    }

    /** 뒤로가기 버튼을 눌렀을 때 */
    public void OnClickBackButton()
    {
        // Do Somthing
    }
    #endregion // 함수
}
