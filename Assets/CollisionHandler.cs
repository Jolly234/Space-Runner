using UnityEngine.SceneManagement;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delayTimer = 3f;
    [SerializeField] AudioClip death;
    [SerializeField] AudioClip success;
    [SerializeField] float audioVolume;

    [SerializeField] ParticleSystem deathp;
    [SerializeField] ParticleSystem successp;

    AudioSource audioSource;
    

    bool isTransitioning = false;
    bool collisionDisabled = false;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {      
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if(Input.GetKeyDown(KeyCode.L)) 
        {
            NextLevel();
        }

        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled; //toggle collision
        }
    }

    void OnCollisionEnter(Collision other)
    {   
        if (isTransitioning || collisionDisabled){ return; }

        
        
        switch (other.gameObject.tag) // switch can be used instead of if/else (conditional)
        {
            case "Friendly":
                Debug.Log("is friendly");
                break;
            case "Finish":
                StartLoadingSequence();
                break;          
            default:
                StartCrashSequence();                  
                break;
        }
    }

        void StartCrashSequence()
        {
            isTransitioning = true;
            audioSource.Stop();
            GetComponent<Move>().enabled = false;
            Invoke("ReloadLevel", delayTimer);
            audioSource.PlayOneShot(death, audioVolume);
            deathp.Play();
        }
       
        void StartLoadingSequence()
        {   
            audioSource.Stop();
            isTransitioning = true;
            GetComponent<Move>().enabled = false;
            Invoke("NextLevel", delayTimer);
            audioSource.PlayOneShot(success, audioVolume);
            successp.Play();
         }

        void NextLevel()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = currentSceneIndex + 1;
            
            if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
            {
                nextSceneIndex = 0;
            }

            SceneManager.LoadScene(nextSceneIndex);       
    }

        void ReloadLevel()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }

    
}
