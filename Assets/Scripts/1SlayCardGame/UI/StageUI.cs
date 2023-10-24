using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageUI : MonoBehaviour
{
    #region 변수
    [SerializeField] private Button OptionSettingButton = null;
    [SerializeField] private Button NextButton = null;
    #endregion // 변수

    #region 함수 
    /** 초기화 */
    private void Awake()
    {
        OptionSettingButton.onClick.AddListener(OnClickShowSetting);
        NextButton.onClick.AddListener(ClickNextTurnButton);
    }

    /** 설정창을 보여준다 */
    public void OnClickShowSetting()
    {
        UIManager.Inst.OptionShow(true);
    }

    /** 턴을 넘기는 버튼을 활성화 한다 */
    public void ClickNextTurnButton()
    {
        if (TurnManager.Instane.oIsMyTurn == true)
        {
            TurnManager.Instane.NextTurn();
        }
    }
    #endregion // 함수
}