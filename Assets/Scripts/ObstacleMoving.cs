using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EMovingType
{
    None = 0,

    Horizental,
    Vertical
}

public class ObstacleMoving : MonoBehaviour
{
    [SerializeField] private EMovingType MovingType = EMovingType.None;
    [SerializeField] private float MovingSpeed = 1.0f;

    private float MovingDirValue = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        float RandomValue = Random.Range(0.0f, 1.0f);
        MovingDirValue *= RandomValue < 0.5f ? -1.0f : 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(MovingType == EMovingType.None)
        {
            return;
        }

        float MovingLength = MovingSpeed * Time.deltaTime * MovingDirValue;

        switch(MovingType)
        {
            case EMovingType.Horizental:
            {
                transform.Translate(MovingLength, 0.0f, 0.0f);
            }
            break;
            case EMovingType.Vertical:
            {
                transform.Translate(0.0f, MovingLength, 0.0f);
            }
            break;
            default:
            break;
        }
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag != "Player")
        {
            MovingDirValue *= -1.0f;
        }
    }
}
