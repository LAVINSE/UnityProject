using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextButtonUI : MonoBehaviour
{
    #region 변수
    private Button NextTurnButton;
    #endregion 변수

    #region 함수
    private void Awake()
    {
        NextTurnButton = GetComponent<Button>();
    }

    private void Update()
    {
        if(TurnManager.Instance.oIsMyTurn == false)
        {
            NextTurnButton.interactable = false;
        }
        else
        {
            NextTurnButton.interactable = true;
        }
    }
    #endregion // 함수
}
