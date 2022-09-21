using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float levelLoadDelay;

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("this object is friendly");
                break;

            case "Finish":
                StartSuccessSequence();
                break;

            default:
                StartCrashSequence();
                break;
        }
    }

    private void StartSuccessSequence()
    {
        GetComponent<RocketMovement>().enabled = false;
        Invoke("LoadNextLevel", 1f);
    }

    private void StartCrashSequence()
    {
        GetComponent<RocketMovement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }

    private void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }
}
