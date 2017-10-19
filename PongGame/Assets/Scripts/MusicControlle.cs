using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicControlle : MonoBehaviour {
    
    public AudioClip Bgm, Click, Hit;
    //需要给物体添加AudioSource组件

    private AudioSource audioSource;
    private static MusicControlle _instance;
    /// <summary>
    /// 使用单例模式控制所有音乐的播放
    /// </summary>
    public  static MusicControlle instance
    {
        get
        {
            return _instance;
        }
    }
    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    private void Awake()
    {
        _instance = this;
    }
    public void PlayBgm()
    {
        audioSource.clip = Bgm;
        audioSource.loop = true;
        audioSource.Play();
    }
    public void PlayHit()
    {
        audioSource.clip = Hit;
        audioSource.Play();
    }
    public void PlayClick()
    {
        audioSource.clip = Click;
        //通过设置随机播放速度，使得每次打击声音都有所不同
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.Play();
    }
}
