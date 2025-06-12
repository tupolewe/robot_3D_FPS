using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadEnding : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            Debug.Log("next level");
            SceneManager.LoadScene("EndingScene");

        }

    }
}
