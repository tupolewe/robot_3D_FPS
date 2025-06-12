
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLvL : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            Debug.Log("next level");
            SceneManager.LoadScene("boltonscena2");

        }
      
    }
}
