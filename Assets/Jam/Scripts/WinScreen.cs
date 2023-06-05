using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    public TextMeshProUGUI starsText;
    
    public void Initialize()
    {
        gameObject.SetActive(true);
        string msg = "Stars: " + JamGameController.instance.currentStars;
        starsText.text = msg;
    }

    public void OnButtonPress()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
