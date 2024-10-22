using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour {
    
    IEnumerator Start()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Menu");
    }
}
