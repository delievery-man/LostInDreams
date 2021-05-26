using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private GameObject player;

    private AudioSource runSound;
    // Start is called before the first frame update
    void Start()
    {
        runSound = player.GetComponent<AudioSource>();
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
