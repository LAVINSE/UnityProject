using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : CSingleton<GameManager>
{
    #region 변수
    [SerializeField] private NotificationPanel oNotificationPanel;
    [SerializeField] private GameObject Popup;
    #endregion // 변수

    #region 프로퍼티
    public ObjectPoolManager PoolManager { get; private set; } = null;
    #endregion // 프로퍼티

    #region 함수
    /** 초기화 */
    public override void Awake()
    {
        base.Awake();
        PoolManager = CFactory.CreateObject<ObjectPoolManager>("ObjectPoolManager", this.gameObject,
            Vector3.zero, Vector3.one, Vector3.zero);
    }

    /** 초기화 */
    public override void Start()
    {
        base.Start();

        StartGame();
    }

    /** 초기화 */
    public override void Update()
    {
        base.Update();

        // 옵션 창
        OptionShow();

        // 에디터에서 사용가능한 치트키
#if UNITY_EDITOR
        InputCheatKey();
#endif // UNITY_EDITOR
    }

    /** 옵션 팝업 콜백을 수신했을 경우 */
    private void OnReceivePopup(OptionPopup Option, bool Isbool)
    {
        // 계속한다 버튼을 눌렀을 경우
        if(Isbool == true)
        {
            Debug.Log("Test");
        }
        // 나가기 버튼을 눌렀을 경우
        else
        {
            Debug.Log("Test2");
        }
    }

    /** 옵션 팝업을 보여준다 */
    public void OptionShow(bool IsClick = false)
    {
        // Esc 키를 눌렀을 경우
        if (Input.GetKeyDown(KeyCode.Escape) || IsClick == true)
        {
            var Option = Popup.GetComponentInChildren<OptionPopup>();

            // 옵션 팝업이 존재 할 경우
            if (Option != null)
            {
                Option.PopupClose();
            }
            else
            {
                Option = OptionPopup.CreateOptionPopup("테스트", Popup);

                Option.PopupShow(OnReceivePopup);
            }
        }
    }


    /** 치트키 키 입력 */
    private void InputCheatKey()
    {
        // 앞면 카드 추가
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TurnManager.IsOnAddCard?.Invoke(true);
        }

        // 뒷면 카드 추가
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            TurnManager.IsOnAddCard?.Invoke(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            TurnManager.Instacne.NextTurn();
        }
    }

    /** 게임을 시작한다 */
    public void StartGame()
    {
        StartCoroutine(TurnManager.Instacne.StartGameCo());
    }

    /** 턴 시작을 알린다 */
    public void Notification(string Message)
    {
        oNotificationPanel.Show(Message);
    }
#endregion // 함수
}
