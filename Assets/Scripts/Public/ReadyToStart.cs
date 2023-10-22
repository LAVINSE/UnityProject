using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyToStart : MonoBehaviour
{
    /** 초기화 */
    private void Awake()
    {
        Time.timeScale = 0.0f;
    }

    /** 게임 시작 버튼을 클릭한다 */
    public void OnClick()
    {
        Time.timeScale = 1.0f;
        this.gameObject.SetActive(false);
        GameManager.Inst.IsGameStart = true;
    }
}
