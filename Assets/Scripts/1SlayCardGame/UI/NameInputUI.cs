using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NameInputUI : MonoBehaviour
{
    #region 변수
    [SerializeField] private TMP_InputField NameInput;
    private string PlayerName = string.Empty;
    #endregion // 변수

    #region 함수
    /** 초기화 */
    private void Awake()
    {
        PlayerName = NameInput.text;
    }

    /** 이름을 저장한다 */
    public void InputNameSave()
    {
        PlayerName = NameInput.text;
        PlayerPrefs.SetString("CurrentPlayerName", PlayerName);
        GameManager.Inst.PlayerNameSave();
    }

    /** 이름 입력창을 닫는다 */
    public void InputClose()
    {
        AudioManager.Inst.PlaySFX(AudioManager.SFXEnum.OptionButton);
        Destroy(this.gameObject);
    }

    /** 이름 입력창을 생성한다 */
    public static NameInputUI CreateNameInput(GameObject RootObject)
    {
        var NameInput = CFactory.CreateCloneObj<NameInputUI>("NameInput",
            Resources.Load<GameObject>("Prefabs/UiPrefabs/NameInput"), RootObject,
            Vector3.zero, Vector3.one * 3f, Vector3.zero);

        return NameInput;
    }
    #endregion // 함수
}
