using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour
{
    public float cd;
    public bool isCd;
    public bool isTest;
    private Image shieldImage;
    private PlayerMovement player;
   
    void Start()
    {
        if (!isTest)
        {
            shieldImage = GetComponent<Image>();
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        }
        isCd = false;
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