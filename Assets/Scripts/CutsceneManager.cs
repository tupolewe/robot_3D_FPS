using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CutsceneManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public VideoClip firstClip;
    public VideoClip secondClip;
    public GameObject video1;
    public GameObject video2;

    private bool hasPlayedSecond = false;

    void Start()
    {
        if (videoPlayer == null || firstClip == null || secondClip == null)
        {
            Debug.LogError("Assign all required components and clips!");
            return;
        }

        videoPlayer.clip = firstClip;
        videoPlayer.loopPointReached += OnVideoFinished;
        videoPlayer.Play();
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        if (!hasPlayedSecond)
        {
            SceneManager.LoadScene("EndingScene2");
        }
        else
        {
            Debug.Log("All cutscenes done. You can now load a new scene or show credits.");
            // For example:
            // SceneManager.LoadScene("MainMenu");
        }
    }
}
