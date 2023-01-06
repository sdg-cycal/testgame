using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerLogic : MonoBehaviour
{
    public int TotalCoinCount;
    public int Stage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player")
            SceneManager.LoadScene(Stage);
    }
}
