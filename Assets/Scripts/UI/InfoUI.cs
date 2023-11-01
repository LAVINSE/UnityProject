using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoUI : MonoBehaviour
{
    #region 변수
    [SerializeField] private TMP_Text PlayerNameText = null;
    [SerializeField] private TMP_Text PlayerHpText = null;
    [SerializeField] private TMP_Text PlayerCostText = null;
    [SerializeField] private Button CancelButton = null;
    #endregion // 변수

    #region 함수
    /** 초기화 */
    private void Awake()
    {
        CancelButton.onClick.AddListener(CloseInfo);
        PlayerNameText.text = $"이름 : {PlayerPrefs.GetString("CurrentPlayerName")}";
        PlayerHpText.text = $"ATK : {GameManager.Inst.oPlayerMaxHp}";
        PlayerCostText.text = $"Cost : {GameManager.Inst.oPlayerMaxCost}";
    }

    /** 정보 UI를 닫는다 */
    public void CloseInfo()
    {
        Destroy(this.gameObject);
    }    

    /** 정보 UI를 생성한다 */
    public static InfoUI CreateInfoUI(GameObject InfoRoot)
    {
        var CreateInfo = CFactory.CreateCloneObj<InfoUI>("Info",
            Resources.Load<GameObject>("Prefabs/UiPrefabs/InfoPanel"), InfoRoot,
            Vector3.zero, Vector3.one, Vector3.zero);

        return CreateInfo;
    }
    #endregion // 함수
}
