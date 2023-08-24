using UnityEngine;
using UnityEngine.SceneManagement; //Gör att scener kan skötas

public class LevelController : MonoBehaviour
{
    private static int _nextLevelIndex = 1; //Variabel som heter "_nextLevelIndex" och som har värdet "1"
    private Target[] _targets; //Skapar en lista av "Target" så att man kan gå igenom var och en

    private void OnEnable() //En metod som kallas på varje gång gameObjektet LevelController är igång
    {
        _targets = FindObjectsOfType<Target>(); //Sparar alla "Target" i en speciell variabel som kallas _targets
    }
    
    // Update is called once per frame
    void Update()
    {
        foreach(Target target in _targets) //Loopar och går igenom varje "Target" och lägger in dem i variabeln "target"
        {
            if (target != null) //if sats som aktiveras om ett "target" i _targets listan inte är förstört
                return; //Hoppar ur Update
        }
        
        //Om alla "target" är förstörda så ska koden under köras
        _nextLevelIndex++; //Ökar värdet i "_nextLevelIndex" med 1
        string nextLevelName = "Level" + _nextLevelIndex; //En variabel som heter "nextLevelName" och dess värde är en text + värdet i variabeln "_nextLevelIndex"
        SceneManager.LoadScene(nextLevelName); //Laddar scenen som motsvarar värdet i "nextLevelName"
        
    }
}
