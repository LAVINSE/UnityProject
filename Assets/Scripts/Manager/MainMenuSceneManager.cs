using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSceneManager : CSceneManager
{
    public GameObject NameInputRoot { get; private set; } = null;

    #region 함수 
    /** 초기화 */
    public override void Awake()
    {
        base.Awake();
        var RootObjs = this.gameObject.scene.GetRootGameObjects();

        for (int i = 0; i < RootObjs.Length; i++)
        {
            this.NameInputRoot = this.NameInputRoot ??
                RootObjs[i].transform.Find("Canvas/NameInputRoot")?.gameObject;
        }

        if(GameManager.Inst.oPlayerName == string.Empty)
        {
            ShowNameInput();
        }
    }

    /** 이름 입력창을 보여준다 */
    public void ShowNameInput()
    {
        var NameInput = NameInputRoot.GetComponentInChildren<NameInputUI>();

        if (NameInput == null)
        {
            AudioManager.Inst.PlaySFX(AudioManager.SFXEnum.OptionButton);
            NameInput = NameInputUI.CreateNameInput(NameInputRoot);
        }
    }
    #endregion // 함수
}