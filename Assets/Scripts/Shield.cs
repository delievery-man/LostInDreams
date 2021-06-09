using System;
using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour
{
    public float cd;
    public bool isCd;
    public bool isTest;
    private Image shieldImage;
    private PlayerMovement player;
<<<<<<< HEAD
    // Start is called before the first frame update
    private void Awake()
    {
        shieldImage = GetComponent<Image>();
    }

=======
   
>>>>>>> 202853a88260d2f4f65e09a45df0789b53b99483
    void Start()
    {
        if (!isTest)
        {
            
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        }
    }

    void Update()
    {
        if (isCd && !isTest)
        {
            shieldImage.fillAmount -= (1 / cd * Time.deltaTime);
            if (shieldImage.fillAmount<=0)
            {
                shieldImage.fillAmount = 1;
                isCd = false;
                player.shield.SetActive(false);
                gameObject.SetActive(false);
            }
        }
    }

    public void ResetTimer()
    {
        shieldImage.fillAmount = 1;
    }
}