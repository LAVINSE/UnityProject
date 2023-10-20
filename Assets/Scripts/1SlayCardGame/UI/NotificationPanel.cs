using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class NotificationPanel : Popup
{
    #region 변수
    [SerializeField] private TMP_Text MyTurnStartTMP; // TMP 변수
    #endregion // 변수

    #region 함수
    /** 초기화 */
    private void Start()
    {
        
    }

    /** 턴 시작 알림창을 종료한다 */
    public void Close()
    {
        Destroy(this.gameObject);
    }

    /** 이미지 크기를 조절한다 */
    public void Show(string Message)
    {
        this.transform.localScale = Vector3.zero;

        MyTurnStartTMP.text = Message;

        // 크기가 0에서 1로 커졌다가 0.9초 대기 후 크기가 0으로 작아진다
        Sequence oSequence = DOTween.Sequence()
            .Append(transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.InOutQuad))
            .AppendInterval(0.9f)
            .Append(transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InOutQuad));

        // 2초후 종료
        Invoke("Close", 2.0f);
    }

    /** 턴 시작 알림창을 생성한다 */
    public static NotificationPanel CreateNotiPanel(GameObject RootObject)
    {
        var CreateNoti = CFactory.CreateCloneObj<NotificationPanel>("NotiPanel",
             Resources.Load<GameObject>("Prefabs/UiPrefabs/NotiPanel"), RootObject,
             Vector3.zero, Vector3.one, Vector3.zero);

        return CreateNoti;
    }
    #endregion // 함수
}
