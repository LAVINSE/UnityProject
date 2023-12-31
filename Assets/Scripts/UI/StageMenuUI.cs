using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageMenuUI : MonoBehaviour
{
    #region 변수
    [Header("=====> Stage Info <=====")]
    [SerializeField] private GameManager.StageInfo[] StageInfoArray = null;

    [Header("=====> Stage UI <=====")]
    [SerializeField] private TMP_Text StageTitleText = null;
    [SerializeField] private TMP_Text PlayerNameText = null;
    [SerializeField] private Image StageImg = null;

    [Header("=====> Stage Title Button <=====")]
    [SerializeField] private Button InfoButton = null;
    [SerializeField] private Button DeckButton = null;
    [SerializeField] private Button MenuButton = null;

    private int CurrentStage = 0;
    #endregion // 변수

    #region 함수
    /** 초기화 */
    private void Awake()
    {
        MenuButton.onClick.AddListener(OnClickPopShow);
        DeckButton.onClick.AddListener(OnClickDeckListShow);
        InfoButton.onClick.AddListener(OnClickInfoShow);
    }

    /** 초기화 */
    private void Start()
    {
        SettingStageInfo();
        AudioManager.Inst.PlayBGM(AudioManager.BGMEnum.MenuBGM);
    }

    /** 초기화 => 상태를 갱신한다 */
    private void Update()
    {
        if(GameManager.Inst.IsChangeName == false && GameManager.Inst.oPlayerName != string.Empty)
        {
            ChangeNameText();
            GameManager.Inst.IsChangeName = true;
        }
    }

    /** 이름 텍스트를 변경한다 */
    private void ChangeNameText()
    {
        PlayerNameText.text = GameManager.Inst.oPlayerName;
    }

    /** Next 스테이지 버튼을 눌렀을 때*/
    public void OnClickNextButton()
    {
        if(++CurrentStage > StageInfoArray.Length - 1)
        {
            CurrentStage = 0;
        }

        AudioManager.Inst.PlaySFX(AudioManager.SFXEnum.NextBackButton);
        SettingStageInfo();
    }

    /** Back 스테이지 버튼을 눌렀을 때*/
    public void OnClickPriorButton()
    {
        if (--CurrentStage < 0)
        {
            CurrentStage = StageInfoArray.Length - 1;
        }
        AudioManager.Inst.PlaySFX(AudioManager.SFXEnum.NextBackButton);
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
        GameManager.Inst.oStageEnemyType = StageInfoArray[CurrentStage].StageEnemyType;
        AudioManager.Inst.PlaySFX(AudioManager.SFXEnum.GameStartButton);

        GameManager.Inst.ChangeScene("SlayCardGame");
    }

    /** 뒤로가기 버튼을 눌렀을 때 */
    public void OnClickBackButton()
    {
        AudioManager.Inst.PlaySFX(AudioManager.SFXEnum.LeaveButton);
    }

    /** 버튼을 눌렀을때 메뉴 버튼을 보여준다 */
    private void OnClickPopShow()
    {
        CSceneManager.Instance.OptionShow(true);
    }

    /** 버튼을 눌렀을때 덱 리스트를 보여준다 */
    private void OnClickDeckListShow()
    { 
        CSceneManager.Instance.DeckListShow();   
    }

    /** 버튼을 눌렀을 때 정보창을 보여준다 */
    private void OnClickInfoShow()
    {
        CSceneManager.Instance.InfoShow();
    }
    #endregion // 함수
}
