using UnityEngine;
using UnityEngine.SceneManagement;


public class CollisionHandler : MonoBehaviour
{   
    [SerializeField] private float invokeTimeForLvlReload = 3.0f;
    [SerializeField] private float invokeTimeForNextLvl = 2.0f;

    [SerializeField] AudioClip explosion;
    [SerializeField] AudioClip success;

    private AudioSource audioSource;

    bool isTransitioning = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(!isTransitioning)
        {
            switch (other.gameObject.tag)
            {
                case "Health" :
                    Debug.Log("Health");
                    break;
                case "Environment" :
                    Debug.Log("Environment");
                    break;
                case "Finish" :
                    Debug.Log("Finish");
                    StartNextLvlSequence();
                    break;
                case "Deadly":
                    Debug.Log("You die!");
                    StartCrashSequence();
                    break;
                default:
                    Debug.Log("Hit default" + other.gameObject.tag);
                    break;
            }
        }
    }

    void DisableMovement()
    {
        GetComponent<MovementRocket>().enabled = false;
    }

    void MuteAudio()
    {
        audioSource.Stop();
    }

    void StartNextLvlSequence()
    {
        isTransitioning = true;
        DisableMovement();
        MuteAudio();
        audioSource.PlayOneShot(success, 1.0f);
        Invoke("LoadNextLevel", invokeTimeForNextLvl);
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        DisableMovement();
        MuteAudio();
        audioSource.PlayOneShot(explosion, 1.0f);
        Invoke("ReloadLvl", invokeTimeForLvlReload);
    }

    void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if(SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings-1)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLvl()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
