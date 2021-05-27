using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour
{
    public float cd;

    public bool isCd;
    public bool isTest;

    private Image shieldImage;
    

    private PlayerMovement player;
    // Start is called before the first frame update
    void Start()
    {
        if (!isTest)
        {
            shieldImage = GetComponent<Image>();
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        }
        isCd = false;
    }

    // Update is called once per frame
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