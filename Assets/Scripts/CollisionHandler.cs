using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float LevelLoadDelayTime = 0.5f;
    [SerializeField] private AudioClip RocketCrashSound;
    [SerializeField] private AudioClip LevelSuccessSound;
    private bool ActivateCollision = true;

    // Start is called before the first frame update
    void Start()
    {
        ActivateCollision = true;
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    private void OnCollisionEnter(Collision other)
    {
        if(!ActivateCollision)
        {
            return;
        }

        switch(other.gameObject.tag)
        {
            case "Goal":
            {
                GameStateChange(true);  
            }
            break;
            case "Fuel":
            {
                Destroy(other.gameObject);
            }
            break;
            default:
            {
                if(other.gameObject.tag != "Start")
                {
                    GameStateChange(false);                    
                }
            }
            break;
        }
    }

    private void GameStateChange(bool IsSuccess)
    {
        ActivateCollision = false;

        RocketMovement Movement = GetComponent<RocketMovement>();
        if(Movement != null)
        {
            Movement.SetDisableComponent();
            Movement.PlayEffectSound(IsSuccess ? LevelSuccessSound : RocketCrashSound);
        }

        if(IsSuccess)
        {
            Rigidbody RocketRigidbody = GetComponent<Rigidbody>();
            if(RocketRigidbody != null)
            {
                RocketRigidbody.freezeRotation = true;
            }
        }

        Invoke(IsSuccess ? "NextLevel" : "RestartLevel", LevelLoadDelayTime);
    }

    private void RestartLevel()
    {
        int CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(CurrentSceneIndex);
    }

    private void NextLevel()
    {
        int NextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        NextSceneIndex = NextSceneIndex >= SceneManager.sceneCountInBuildSettings ? 0 : NextSceneIndex;

        SceneManager.LoadScene(NextSceneIndex);
    }
}
