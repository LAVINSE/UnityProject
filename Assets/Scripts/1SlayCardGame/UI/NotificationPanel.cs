using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class NotificationPanel : MonoBehaviour
{
    #region 변수
    [SerializeField] private TMP_Text MyTurnStartTMP; // TMP 변수
    [SerializeField] private GameObject asdf;
    #endregion // 변수

    #region 함수
    /** 초기화 */
    private void Start()
    {
        ScaleZero();
    }

    /** 이미지 크기를 조절한다 */
    public void Show(string Message)
    {
        MyTurnStartTMP.text = Message;

        // 크기가 0에서 1로 커졌다가 0.9초 대기 후 크기가 0으로 작아진다
        Sequence oSequence = DOTween.Sequence()
            .Append(transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.InOutQuad))
            .AppendInterval(0.9f)
            .Append(transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InOutQuad));
    }


    /** 인스펙터 우클릭 크기 조절을 생성한다 */
    [ContextMenu("ScaleZero")]
    private void ScaleZero()
    {
        transform.localScale = Vector3.zero;
    }

    /** 인스펙터 우클릭 크기 조절을 생성한다 */
    [ContextMenu("ScaleOne")]
    private void ScaleOne()
    {
        transform.localScale = Vector3.one;
    }
    #endregion // 함수
}
