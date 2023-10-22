using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    #region 변수
    [SerializeField] private Button OptionSettingButton = null;
    #endregion // 변수

    #region 함수 
    /** 초기화 */
    private void Awake()
    {
        OptionSettingButton.onClick.AddListener(Setting);
    }

    public void Setting()
    {
        UIManager.Inst.OptionShow(true);
    }
    #endregion // 함수
}
