using UnityEngine;

public class shotTimer : MonoBehaviour
{
    public TextMesh myText;

    public int timeLeft;

    private float gameTime;
    // Start is called before the first frame update

    // Update is called once per frame
    private float nextActionTime = 0.0f;
    public bool isCd;
    public float period = 0.1f;

    void Update ()
    {
        if (isCd)
        {
            myText.text = timeLeft.ToString();
            gameTime += 1 * Time.deltaTime;
            if (gameTime>=1)
            {
                timeLeft--;
                gameTime = 0;
            }
            else if(timeLeft<=0)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().weapon = Shooting.WeaponTypes.Single;
                gameObject.SetActive(false);
                isCd = false;
                timeLeft = 10;

            }
        }
       
    }

    public void ResetTimer()
    {
        timeLeft = 10;
    }
}
