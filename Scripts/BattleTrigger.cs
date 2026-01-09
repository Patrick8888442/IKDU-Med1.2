using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player")){
            SceneManager.LoadScene("TestCombat");
        }
    } 
    
}
