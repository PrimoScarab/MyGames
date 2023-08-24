using UnityEngine;

public class cannonBall : MonoBehaviour
{
    float timer = 0; //Variabel som har värdet "0"

    [SerializeField] //Gör så att man kan skystera värdet i unity
    float timeToLive = 5; //Variabel som har värdet "5"

    
    // Update is called once per frame
    void Update()
    {
            if (transform.position.y < -10.0f) //if-sats som aktiveras om objektets position i y-led är mindre än "-10.0"
        {
            Destroy(gameObject); //Förstör objektet
        }

        timer += Time.deltaTime; //Adderar värdet i timer med Time.deltaTime, alltså tiden som passerat sedan senaste framen. 

        if(timer > timeToLive) //if-sats som aktiveras om värdet i timer är större än värdet i timeToLive
        {
            Destroy(gameObject); //Förstör objektet
        }
    }


    private void OnCollisionEnter2D(Collision2D collision) //En metod som talar om när "cannonBall" blir kolliderat med. "Collision2D" är ett objekt och "collision" är dess namn.
    {
        if (collision.collider.GetComponent<Target>() != null) //if sats som aktiveras om "cannonBall" har kolliderat med en "Target", om det inte är fallet så ska resulatet vara null
        {
            Destroy(gameObject); //Förstör gameObjektet
        }
    }
}
