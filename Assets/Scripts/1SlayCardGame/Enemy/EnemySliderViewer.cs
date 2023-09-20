using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemySliderViewer : MonoBehaviour
{
    #region 변수
    [SerializeField] private TMP_Text EnemyHpText;

    private EnemySetting EnemyHp;
    private Slider HpSlider;
    #endregion // 변수

    #region 함수
    private void Update()
    {
        EnemyHpViewerUpdate();

        if(EnemyHp.oCurrentHp <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    /** 체력 정보를 HpSlider에 세팅한다 */
    private void EnemyHpViewerUpdate()
    {
        HpSlider.maxValue = EnemyHp.oMaxHp;
        HpSlider.value = EnemyHp.oCurrentHp;
        EnemyHpText.text = (EnemyHp.oCurrentHp.ToString() + "/" + EnemyHp.oMaxHp.ToString());
    }

    /** 적 체력 정보를 가져온다 */
    public void Setup(EnemySetting EnemyHp)
    {
        this.EnemyHp = EnemyHp;
        HpSlider = GetComponent<Slider>();
    }
    #endregion // 함수
}
