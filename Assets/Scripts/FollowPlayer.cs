using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Player;
    public Vector3 offset;
    public float smoothing;

    // Update is called once per frame
    void Update()
    {
        if (Player != null)
        {
            var newPos = Vector3.Lerp(transform.position, Player.transform.position + offset, smoothing);
            transform.position = newPos;
        }
        
    }
}
