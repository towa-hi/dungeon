using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoPanel : MonoBehaviour
{
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI starCount;
    public TextMeshProUGUI manaCount;

    private bool initialized = false;

    public void Initialize()
    {
        initialized = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (initialized)
        {
            levelText.SetText(JamGameController.instance.mapController.currentLevel.levelNumber.ToString());
            starCount.SetText(JamGameController.instance.currentStars.ToString());
            manaCount.SetText(JamGameController.instance.currentMana.ToString());
        }
    }
}
