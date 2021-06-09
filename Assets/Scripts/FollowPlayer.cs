using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform Player;
    public Vector3 offset;
    public float smoothing;

    void Update()
    {
        if (!(Player is null))
        {
            var newPos = Vector3.Lerp(transform.position, Player.transform.position + offset, smoothing);
            transform.position = newPos;
        }
        
    }
}
