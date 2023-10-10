using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region 변수
    [Header("=====> 턴 시작 알림 <=====")]
    [SerializeField] private NotificationPanel oNotificationPanel;

    [Header("=====> UI 객체, 객체 위치 <=====")]
    [SerializeField] private GameObject PopupRoot;
    [SerializeField] private GameObject DropUIRoot;
    [SerializeField] private GameObject LeaveButton;
    [SerializeField] private GameObject DeckListShowObject;

    [Header("=====> UI 텍스트 입력 <=====")]
    [SerializeField] private string OptionTitleText;
    #endregion // 변수

    #region 프로퍼티
    public static UIManager Instance { get; private set; }
    public bool IsDeckListShow = false;
    #endregion // 프로퍼티

    #region 함수
    /** 초기화 */
    private void Awake()
    {
        Instance = this;
    }

    /** 초기화 >> 상태를 갱신한다 */
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
                AudioManager.Instance.PlaySFX(AudioManager.SFXEnum.OptionButton);
                Option = OptionPopup.CreateOptionPopup(OptionTitleText, PopupRoot);

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

    /** 드랍 UI 나가기 버튼을 활성화 한다 */
    private void ActiveLeaveButton()
    {
        LeaveButton.SetActive(true);
    }

    /** 턴 시작을 알린다 */
    public void Notification(string Message)
    {
        oNotificationPanel.Show(Message);
    }

    /** 드랍 UI를 보여준다 */
    public void ShowDropUI()
    {
        var GetDropUI = DropUIRoot.GetComponentInChildren<DropUI>();

        // 나가기 버튼을 보여준다
        ActiveLeaveButton();

        // DropUI가 존재하지 않을 경우
        if (GetDropUI == null)
        {
            GetDropUI = DropUI.CreateDropUI(DropUIRoot);

            GetDropUI.DropShow();
        }

    }

    /** 버튼을 누를경우 덱 리스트를 보여준다 */
    public void ShowDeckList()
    {
        IsDeckListShow = true;
        CardManager.Instance.CardDeckCreate();
        DeckListShowObject.SetActive(true);
    }
    #endregion // 함수
}
