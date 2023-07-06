using UnityEngine.SceneManagement;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{   
     
    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag) // switch can be used instead of if/else (conditional)
        {
            case "Friendly":
                Debug.Log("is friendly");
                break;
            case "Finish":
                Debug.Log("finish line reached");
                break;
            case "Fuel":
                Debug.Log("refueled");
                break;
            default:
                ReloadLevel();
                break;
        }

        void ReloadLevel()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }


    }
}
