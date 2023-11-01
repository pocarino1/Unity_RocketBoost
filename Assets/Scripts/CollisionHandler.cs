using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float LevelLoadDelayTime = 0.5f;
    [SerializeField] private AudioClip RocketCrashSound = null;
    [SerializeField] private AudioClip LevelSuccessSound = null;
    [SerializeField] private ParticleSystem LevelSuccessParticleEffect = null;
    [SerializeField] private ParticleSystem RocketCrashParticleEffect = null;

    private bool IsGameStateTransitioning = false;
    private bool IsActivateCollision = true;

    // Start is called before the first frame update
    void Start()
    {
        IsGameStateTransitioning = false;
        IsActivateCollision = true;
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    private void RespondToDebugKeys()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            NextLevel();
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            IsActivateCollision = !IsActivateCollision;
        }
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    private void OnCollisionEnter(Collision other)
    {
        if(IsGameStateTransitioning || !IsActivateCollision)
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
        IsGameStateTransitioning = true;

        RocketMovement Movement = GetComponent<RocketMovement>();
        if(Movement != null)
        {
            Movement.SetDisableComponent();

            if(IsSuccess)
            {
                Movement.PlayEffectSound(LevelSuccessSound);

                if(LevelSuccessParticleEffect != null)
                {
                    LevelSuccessParticleEffect.Play();
                }
            }
            else
            {
                Movement.PlayEffectSound(RocketCrashSound);

                if(RocketCrashParticleEffect != null)
                {
                    RocketCrashParticleEffect.Play();
                }
            }
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
