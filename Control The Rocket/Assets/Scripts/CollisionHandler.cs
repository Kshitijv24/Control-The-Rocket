using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float levelLoadDelay;
    [SerializeField] private AudioClip deathSFX;
    [SerializeField] private AudioClip winSfX;
    [SerializeField] private ParticleSystem deathParticle;
    [SerializeField] private ParticleSystem winParticle;

    private AudioSource audioSource;
    private bool isTransitioning = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(isTransitioning == true)
        {
            return;
        }

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
        audioSource.Stop();
        winParticle.Play();
        
        if (!audioSource.isPlaying && isTransitioning == false)
        {
            audioSource.PlayOneShot(winSfX);
            isTransitioning = true;
        }

        Invoke("LoadNextLevel", 1f);
    }

    private void StartCrashSequence()
    {
        GetComponent<RocketMovement>().enabled = false;
        audioSource.Stop();
        deathParticle.Play();

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(deathSFX);
            isTransitioning = true;
        }

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
