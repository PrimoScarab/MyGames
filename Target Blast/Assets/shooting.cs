using TMPro; //Gör att man kan använda TextMeshPro
using UnityEngine;

public class shooting : MonoBehaviour
{
    public Transform firePoint; //Variabel som heter firePoint. En referens till objektet firePoint
    public GameObject CannonBallPrefab; //Variabel som heter CannonBallPrefab. En referens till GameObjektet CannonBallPrefab

    public float CannonBallForce = 2f; //Variabel som heter CannonBallForce och ger den värdet 2

    [SerializeField] //Gör att man kan skystera värdet i unity
    TextMeshProUGUI powerValue; //Gör en TextMeshProUGUI som kallas powerValue


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Jump")) //if sats som aktiveras om mellanslagstangenten åker up
        {
            Shoot(); //Kallar på metoden Shoot
            CannonBallForce = 2f; //Återställer värdet i CannonBallForce efter varje skott
        }

        if (Input.GetButton("Jump")) //if sats som aktiveras om mellanslagstangenten trycks ner
        {
            CannonBallForce += Time.deltaTime * 10; //Ökar värdet i CannonBallForce var tiondels sekund
            powerValue.text = ((int)CannonBallForce).ToString(); //Sätter värdet i powerValue's text till värdet i CannonBallForce och konverterar det till en sträng
        }

    }


    void Shoot() //En metod som heter Shoot
    {
       GameObject CannonBall = Instantiate(CannonBallPrefab, firePoint.position, firePoint.rotation); //Skapar en kanonkula och ger den samma position och rotation som firePoint. GameObjekt CannonBall är en variabel som gör att man kan referera till kanonkulan och därmed kunna modifiera den senare.
        Rigidbody2D rb = CannonBall.GetComponent<Rigidbody2D>(); //Gör en variabel som heter rb och dess värde blir detsamma som CannonBall's Rigidbody2D
        rb.AddForce(firePoint.right * CannonBallForce, ForceMode2D.Impulse); //Lägger till funktionen AddForce på rb. Här läggs till en kraft i firePoint's höger vektors riktning. Kraften gångas med CannonBallForce. Kraftläget är Impulse eftersom Rigidbody2D ska ha en omedelbar kraftimpuls.
    }
}
