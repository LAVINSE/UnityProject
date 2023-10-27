using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageUI : MonoBehaviour
{
    #region 변수
    [SerializeField] private Button OptionSettingButton = null;
    [SerializeField] private Button OptionDeckButton = null;
    [SerializeField] private Button NextButton = null;
    [SerializeField] private TMP_Text PlayerNameText = null;
    #endregion // 변수

    #region 함수 
    /** 초기화 */
    private void Awake()
    {
        OptionSettingButton.onClick.AddListener(OnClickShowSetting);
        NextButton.onClick.AddListener(ClickNextTurnButton);
        OptionDeckButton.onClick.AddListener(OnClickShowDeckList);

        PlayerNameText.text = GameManager.Inst.oPlayerName;
    }

    /** 설정창을 보여준다 */
    public void OnClickShowSetting()
    {
        CSceneManager.Instance.OptionShow(true);
    }

    /** 덱 리스트를 보여준다 */
    public void OnClickShowDeckList()
    {
        CSceneManager.Instance.DeckListShow();
    }

    /** 턴을 넘기는 버튼을 활성화 한다 */
    public void ClickNextTurnButton()
    {
        if (TurnManager.Instance.oIsMyTurn == true)
        {
            TurnManager.Instance.NextTurn();
        }
    }
    #endregion // 함수
}