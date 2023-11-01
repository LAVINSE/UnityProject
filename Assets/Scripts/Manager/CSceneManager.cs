using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CSceneManager : MonoBehaviour
{
    #region 프로퍼티
    public static CSceneManager Instance { get; set; }
    public GameObject PopupRoot { get; private set; } = null;
    #endregion // 프로퍼티

    #region 함수
    /** 초기화 */
    public virtual void Awake()
    {
        var RootObjs = this.gameObject.scene.GetRootGameObjects();

        for (int i = 0; i < RootObjs.Length; i++)
        {
            this.PopupRoot = this.PopupRoot ??
                RootObjs[i].transform.Find("Canvas/PopupRoot")?.gameObject;

        }

        Instance = this;
    }


    /** 초기화 => 상태를 갱신한다 */
    private void Update()
    {
        OptionShow();
    }

    /** 옵션 팝업을 보여준다 */
    public void OptionShow(bool IsClick = false)
    {
        // Esc 키를 눌렀을 경우
        if (Input.GetKeyDown(KeyCode.Escape) || IsClick == true)
        {
            var Option = PopupRoot.GetComponentInChildren<OptionPopup>();

            // 옵션 팝업이 존재 할 경우
            if (Option != null)
            {
                Option.PopupClose();
            }
            else
            {
                AudioManager.Inst.PlaySFX(AudioManager.SFXEnum.OptionButton);
                Option = OptionPopup.CreateOptionPopup("옵션", PopupRoot);

                Option.PopupShow(OnReceivePopup);
            }
        }
    }

    /** 옵션 팝업 콜백을 수신했을 경우 */
    private void OnReceivePopup(OptionPopup Option, bool Isbool)
    {
        // 계속한다 버튼을 눌렀을 경우
        if (Isbool == true)
        {
            // Do Somthing
        }
        // 나가기 버튼을 눌렀을 경우
        else
        {
            // Do Somthing
        }
    }

    /** 나가기 패널을 보여준다 */
    public void LeavePanelShow()
    {
        var LeavePanel = PopupRoot.GetComponentInChildren<LeaveUI>();

        // 나가기 패널이 존재 할 경우
        if (LeavePanel != null)
        {
            LeavePanel.PopupClose();
        }
        else
        {
            AudioManager.Inst.PlaySFX(AudioManager.SFXEnum.OptionButton);
            LeavePanel = LeaveUI.CreateLeavePanel(PopupRoot);
        }
    }

    /** 턴 시작을 알림을 보여준다 */
    public void Notification(string Message)
    {
        var NotiPanel = PopupRoot.GetComponentInChildren<NotificationPanel>();

        // 턴 시작 알림창이 없을 경우
        if (NotiPanel == null)
        {
            NotiPanel = NotificationPanel.CreateNotiPanel(PopupRoot);

            NotiPanel.Show(Message);
        }
    }

    /** 정보 UI를 보여준다 */
    public void InfoShow()
    {
        var InfoPanel = PopupRoot.GetComponentInChildren<InfoUI>();
        
        if(InfoPanel == null)
        {
            InfoPanel = InfoUI.CreateInfoUI(PopupRoot);
        }
    }

    /** 드랍 UI를 보여준다 */
    public void DropUIShow()
    {
        var DropUIPanel = PopupRoot.GetComponentInChildren<DropUI>();

        // DropUI가 존재하지 않을 경우
        if (DropUIPanel == null)
        {
            DropUIPanel = DropUI.CreateDropUI(PopupRoot);

            DropUIPanel.DropShow();
        }
    }

    /** 덱 리스트를 보여준다 */
    public void DeckListShow()
    {
        var DeckList = PopupRoot.GetComponentInChildren<DeckListUI>();

        if (DeckList != null)
        {
            DeckList.PopupClose();
        }
        else
        {
            AudioManager.Inst.PlaySFX(AudioManager.SFXEnum.OptionButton);
            DeckList = DeckListUI.CreateDeckList(PopupRoot);
        }
    }
    #endregion // 함수
}
