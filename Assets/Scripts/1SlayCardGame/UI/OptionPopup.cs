using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionPopup : Popup
{
    /** 매개 변수 */
    public struct OptionParams
    {
        public string p_Title;
        public string p_ContinueText;
        public string p_CancelText;
    }

    #region 변수
    [Header("=====> Option UIs <=====")]
    [SerializeField] private TMP_Text TitleText = null;
    [SerializeField] private TMP_Text BGMVolumeText = null;
    [SerializeField] private TMP_Text SFXVolumeText = null;
    [SerializeField] private Button ContinueButton = null;
    [SerializeField] private Button CancelButton = null;
    [SerializeField] private Slider BGMVolumeSlider = null;
    [SerializeField] private Slider SFXVolumeSlider = null;
    [SerializeField] private GameObject BackGround = null;
    [SerializeField] private string BGMVolume_Text = string.Empty;
    [SerializeField] private string SFXVolume_Text = string.Empty;

    private System.Action<OptionPopup, bool> OptionAction = null;
    #endregion // 변수

    #region 프로퍼티
    public OptionParams oParams { get; private set; }
    #endregion // 프로퍼티

    #region 함수
    /** 초기화 */
    private void Awake()
    {
        Option_Background_Img = BackGround;

        // 버튼을 설정한다
        ContinueButton.onClick.AddListener(OnClickContinue);
        CancelButton.onClick.AddListener(OnClickCancel);

        // 텍스트를 설정한다
        BGMVolumeText.text = BGMVolume_Text;
        SFXVolumeText.text = SFXVolume_Text;

        // 슬라이더를 설정한다
        BGMVolumeSlider.value = AudioManager.Instance.oBGMVolume;
        SFXVolumeSlider.value = AudioManager.Instance.oSFXVolume;
        BGMVolumeSlider.onValueChanged.AddListener(BGMSliderValue);
        SFXVolumeSlider.onValueChanged.AddListener(SFXSliderValue);
    }

    /** Option 초기화 */
    private void Init(OptionParams InitOptionParams)
    {
        oParams = InitOptionParams;
        UpdateOptionState();
    }

    /** Option을 출력한다 */
    public void PopupShow(System.Action<OptionPopup, bool> OptionAction)
    {
        base.PopupShow();
        this.OptionAction = OptionAction;
    }

    /** Option 상태를 갱신한다 */
    private void UpdateOptionState()
    {
        TitleText.text = oParams.p_Title;

        var ContinueButtonText = ContinueButton.GetComponentInChildren<TMP_Text>();
        ContinueButtonText.text = oParams.p_ContinueText;

        var CancelButtonText = CancelButton.GetComponentInChildren<TMP_Text>();
        CancelButtonText.text = oParams.p_CancelText;
    }

    /** ContinueButton 눌렀을 경우 */
    private void OnClickContinue()
    {
        OptionAction?.Invoke(this, true);
        AudioManager.Instance.PlaySFX(AudioManager.SFXEnum.OptionButton);
        PopupClose();
    }

    /** CancelButton 눌렀을 경우 */
    private void OnClickCancel()
    {
        OptionAction?.Invoke(this, false);
        AudioManager.Instance.PlaySFX(AudioManager.SFXEnum.OptionButton);
        PopupClose();
    }

    /** 베경음 슬라이더 핸들을 움직였을 경우 */
    private void BGMSliderValue(float Volume)
    {
        AudioManager.Instance.oBGMVolume = Volume;
    }

    /** 효과음 슬라이더 핸들을 움직였을 경우 */
    private void SFXSliderValue(float Volume)
    {
        AudioManager.Instance.oSFXVolume = Volume;
    }

    /** 매개 변수를 생성한다 */
    public static OptionParams MakeParams(string Title, string ContinueText, string CancleText)
    {
        return new OptionParams()
        {
            p_Title = Title,
            p_ContinueText = ContinueText,
            p_CancelText = CancleText
        };
    }

    /** 옵션 팝업을 생성한다 */
    public static OptionPopup CreateOptionPopup(string TItleMsg, GameObject ParentObject)
    {
        var Params = OptionPopup.MakeParams(TItleMsg, "계속", "나가기");
        var CreateOption = CFactory.CreateCloneObj<OptionPopup>("OptionPopup",
            Resources.Load<GameObject>("Prefabs/UiPrefabs/Option_Popup"), ParentObject,
            Vector3.zero, Vector3.one, Vector3.zero);

        CreateOption.Init(Params);
        return CreateOption;
    }
    #endregion // 함수
}
