using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private GameObject _cloudParticlePrefab; //Refererar till gameObjektet "_cloudParticlePrefab", SerializedField gör så att det kan skysteras i unity

    private void OnCollisionEnter2D(Collision2D collision) //En metod som talar om när "Target" blir kolliderat med. "Collision2D" är ett objekt och "collision" är dess namn.
    {
        if (collision.collider.GetComponent<cannonBall>() != null) //if sats som aktiveras om "Target" har kolliderat med en "cannonBall", om det inte är fallet så ska resulatet vara null
        {
            Instantiate(_cloudParticlePrefab, transform.position, Quaternion.identity); //Placerar en "_cloudParticlePrefab" på samma plats som kollisionen inträffade
            Destroy(gameObject); //Förstör gameObjektet "Target"
        }
    }
}
