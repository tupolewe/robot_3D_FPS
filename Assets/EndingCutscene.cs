using UnityEngine;
using UnityEngine.Video;

public class EndingCutscene : MonoBehaviour
{
    public VideoPlayer videoPlayer; 

    private bool hasPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasPlayed) return;

        Debug.Log("cutscene");
        if (other.CompareTag("Player"))
        {
            Debug.Log("cutscene2");
            hasPlayed = true;
            videoPlayer.Play();
        }
    }
}
