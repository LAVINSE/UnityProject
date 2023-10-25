using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSliderViewer : MonoBehaviour
{
    #region 변수
    [SerializeField] private TMP_Text EnemyHpText;

    private PlayerData PlayerHp;
    private Slider HpSlider;
    #endregion // 변수

    #region 함수
    private void Update()
    {
        EnemyHpViewerUpdate();

        if (PlayerHp.oCurrentHp <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    /** 체력 정보를 HpSlider에 세팅한다 */
    private void EnemyHpViewerUpdate()
    {
        HpSlider.maxValue = PlayerHp.oMaxHp;
        HpSlider.value = PlayerHp.oCurrentHp;
        EnemyHpText.text = (PlayerHp.oCurrentHp.ToString() + "/" + PlayerHp.oMaxHp.ToString());
    }

    /** 적 체력 정보를 가져온다 */
    public void Setup(PlayerData PlayerHp)
    {
        this.PlayerHp = PlayerHp;
        HpSlider = GetComponent<Slider>();
    }
    #endregion // 함수
}
