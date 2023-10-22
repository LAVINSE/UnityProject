using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : CSingleton<UIManager>
{
    #region 변수
    [Header("=====> UI 객체, 객체 위치 <=====")]
    [SerializeField] private GameObject DeckListShowObject;

    [Header("=====> UI 텍스트 입력 <=====")]
    [SerializeField] private string OptionTitleText;
    #endregion // 변수

    #region 프로퍼티
    public GameObject oPopupRoot { get; set; }
    public GameObject oDropUIRoot { get; set; }
    public GameObject oLeavePanelRoot { get; set; }
    public GameObject oDeckListShowRoot { get; set; }
    public GameObject oNotiPanelRoot { get; set; }
    #endregion // 프로퍼티

    #region 함수
    /** 초기화 */
    public override void Awake()
    {
        base.Awake();
        Setting();
    }

    /** 초기화 >> 상태를 갱신한다 */
    private void Update()
    {
        OptionShow();

        // TODO : 수정해야됨, 씬 매니저 만들어서 상속 구조로 설정 
        Setting();
    }

    /** 옵션 팝업을 보여준다 */
    public void OptionShow(bool IsClick = false)
    {
        // Esc 키를 눌렀을 경우
        if (Input.GetKeyDown(KeyCode.Escape) || IsClick == true)
        {
            var Option = oPopupRoot.GetComponentInChildren<OptionPopup>();

            // 옵션 팝업이 존재 할 경우
            if (Option != null)
            {
                Option.PopupClose();
            }
            else
            {
                AudioManager.Inst.PlaySFX(AudioManager.SFXEnum.OptionButton);
                Option = OptionPopup.CreateOptionPopup(OptionTitleText, oPopupRoot);

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
        var LeavePanel = oLeavePanelRoot.GetComponentInChildren<LeaveUI>();

        // 나가기 패널이 존재 할 경우
        if (LeavePanel != null)
        {
            LeavePanel.PopupClose();
        }
        else
        {
            AudioManager.Inst.PlaySFX(AudioManager.SFXEnum.OptionButton);
            LeavePanel = LeaveUI.CreateLeavePanel(oLeavePanelRoot);
        }
    }

    /** 턴 시작을 알림을 보여준다 */
    public void Notification(string Message)
    {
        var NotiPanel = oNotiPanelRoot.GetComponentInChildren<NotificationPanel>();

        // 턴 시작 알림창이 없을 경우
        if(NotiPanel == null)
        {
            NotiPanel = NotificationPanel.CreateNotiPanel(oNotiPanelRoot);

            NotiPanel.Show(Message);
        }
    }

    /** 드랍 UI를 보여준다 */
    public void DropUIShow()
    {
        var DropUIPanel = oDropUIRoot.GetComponentInChildren<DropUI>();

        // DropUI가 존재하지 않을 경우
        if (DropUIPanel == null)
        {
            DropUIPanel = DropUI.CreateDropUI(oDropUIRoot);

            DropUIPanel.DropShow();
        }
    }

    /** 덱 리스트를 보여준다 */
    public void DeckListShow()
    {
        var DeckList = oDeckListShowRoot.GetComponentInChildren<DeckListUI>();

        if(DeckList != null)
        {
            DeckList.PopupClose();
        }
        else
        {
            DeckList = DeckListUI.CreateDeckList(oDeckListShowRoot);
        }
    }

    /** 덱 버튼을 누를경우 덱 리스트를 보여준다 */
    public void ShowDeckList()
    {
        DeckListShow();
    }

    /** 객체 초기화 */
    public void Setting()
    {
        oPopupRoot = GameObject.Find("Canvas/PopupRoot");
        oDropUIRoot = GameObject.Find("Canvas/DropUIRoot");
        oLeavePanelRoot = GameObject.Find("Canvas/LeavePanelRoot");
        oDeckListShowRoot = GameObject.Find("Canvas/DeckListShowRoot");
        oNotiPanelRoot = GameObject.Find("Canvas/NotiPanelRoot");
    }
    #endregion // 함수
}
