using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryUI : MonoBehaviour
{
  public void ReturnToWorld(){
    SceneManager.LoadScene("SampleScene");
  }
}
