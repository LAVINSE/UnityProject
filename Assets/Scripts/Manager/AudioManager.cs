using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region 열거형
    public enum BGMEnum
    {
        MenuBGM,
        Battle_1_BGM,
    }

    public enum SFXEnum
    {
        OptionButton = 0,
        LeaveButton,
        NextBackButton,
        GameStartButton,
        GameOver,
    }
    #endregion 열거형

    #region 변수
    [Header("=====> BGM Setting <=====")]
    [Tooltip(" 배경음 등록 ")] [SerializeField] private AudioClip[] BGMClips; // 배경음
    [Tooltip(" 0 ~ 1 ")] [SerializeField] private float BGMVolume = 0.0f; // 배경음 음량

    [Header("=====> SFX Setting <=====")]
    [Tooltip(" 효과음 등록 ")] [SerializeField] private AudioClip[] SFXClips; // 효과음
    [Tooltip(" 0 ~ 1 ")] [SerializeField] private float SFXVolume = 0.0f; // 효과음 음량
    [Tooltip(" 효과음 채널 개수 ")] [SerializeField] private int SFXChannel = 0;

    private AudioSource[] BGMPlayers;
    private AudioSource[] SFXPlayers;
    private int ChannelIndex;
    #endregion // 변수

    #region 프로퍼티
    public static AudioManager Instance;
    #endregion // 프로퍼티

    #region 함수
    /** 초기화 */
    private void Awake()
    {
        Instance = this;
        BGMInit();
        SFXInit();
    }

    /** 초기화 >> 상태를 갱신한다 */
    private void Update()
    {
        SFXSettingVolume(SFXVolume);
    }

    /** 배경음을 세팅한다 */
    private void BGMInit()
    {
        GameObject BGMObject = new GameObject("BGMPlayer");
        BGMObject.transform.parent = this.transform;
        BGMPlayers = new AudioSource[BGMClips.Length];

        for(int i =0; i < BGMPlayers.Length; i++)
        {
            BGMPlayers[i] = BGMObject.AddComponent<AudioSource>();
            BGMPlayers[i].playOnAwake = false;
            BGMPlayers[i].loop = true;
            BGMPlayers[i].volume = BGMVolume;
            BGMPlayers[i].clip = BGMClips[i];
        }
    }

    /** 효과음을 세팅한다 */
    private void SFXInit()
    {
        GameObject SFXObject = new GameObject("SFXPlayer");
        SFXObject.transform.parent = this.transform;
        SFXPlayers = new AudioSource[SFXChannel];

        for(int i=0; i< SFXPlayers.Length; i++)
        {
            SFXPlayers[i] = SFXObject.AddComponent<AudioSource>();
            SFXPlayers[i].playOnAwake = false;
            SFXPlayers[i].volume = SFXVolume;
        }
    }

    /** 효과음을 재생한다 */
    public void PlaySFX(SFXEnum SFXType)
    {
        for (int i=0; i< SFXPlayers.Length; i++)
        {
            int LoopIndex = (i + ChannelIndex) % SFXPlayers.Length;
            
            // 효과음이 재생중일 경우
            if (SFXPlayers[LoopIndex].isPlaying)
            {
                continue;
            }

            ChannelIndex = LoopIndex;
            SFXPlayers[LoopIndex].clip = SFXClips[(int)SFXType];
            SFXPlayers[LoopIndex].Play();
            break;
        }
    }

    /** 효과음 볼륨을 세팅한다 */
    public void SFXSettingVolume(float SFXVolume)
    {
        for(int i = 0; i < SFXPlayers.Length; i++)
        {
            SFXPlayers[i].volume = SFXVolume;
        }
    }
    #endregion // 함수
}
