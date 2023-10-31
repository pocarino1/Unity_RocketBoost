using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    private void OnCollisionEnter(Collision other)
    {
        switch(other.gameObject.tag)
        {
            case "Start":
            {
                Debug.Log("Rocket is Start Point!");
            }
            break;
            case "Goal":
            {
                Debug.Log("Rocket is Goal Point!");
            }
            break;
            case "Fuel":
            {
                Debug.Log("Pick Up Fuel!");
                Destroy(other.gameObject);
            }
            break;
            default:
            {
                Debug.Log("Crash! Rocket Destroy!");
                Invoke("RestartGame", 1.0f);
            }
            break;
        }
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
