/*==============================================================================
    Priject_産学連携
    [CMovieCIntroller.cs]
    ・動画の一時停止、再生

--------------------------------------------------------------------------------
    2021.07.05 @Author Hirano Tomoki
================================================================================
    History
        
/*============================================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
 
public class CMovieController : MonoBehaviour {
	[SerializeField]private VideoClip vcVideoClip;
	[SerializeField]private GameObject gScreen;
	[SerializeField] VideoPlayer videoPlayer;

	void Start() {
		VideoPlayer vpVideoPlayer = gScreen.AddComponent<VideoPlayer>();  // videoPlayerコンポーネントの追加

		vpVideoPlayer.source = VideoSource.VideoClip;               // 動画ソースの設定
		vpVideoPlayer.clip = vcVideoClip;

		vpVideoPlayer.isLooping = false;                            // ループの設定

		videoPlayer.loopPointReached += LoopPointReached;
		videoPlayer.Play();
	}

	public void Play_Pause() {
		VideoPlayer vpVideoPlayer = GetComponent<VideoPlayer>();

		if (!vpVideoPlayer.isPlaying)	// ボタンを押した時の処理
			vpVideoPlayer.Play();		// 動画を再生する。
		else
			vpVideoPlayer.Pause();		// 動画を一時停止する。
	}

	public void LoopPointReached(VideoPlayer vp)
    {
		CSceneManager CSM = GameObject.Find("FadeCanvas").GetComponent<CSceneManager>();
		CSM.OnChangeScene_Title();
	}
}