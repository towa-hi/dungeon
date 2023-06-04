using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FormPanel : MonoBehaviour
{
    public Vector2Int pos;
    public FormDef form;
    public Image squibby;
    public Image heart1;
    public Image heart2;
    public Image heart3;
    public List<Image> hearts;
    public TextMeshProUGUI manaValue;
    public TextMeshProUGUI formName;
    public bool initialized = false;
    public void Initialize(FormDef form)
    {
        this.initialized = true;
        this.form = form;
        squibby.sprite = form.formSprite;
        heart1.enabled = false;
        heart2.enabled = false;
        heart3.enabled = false;
        UpdateHearts(form.maxHp, form.maxHp);
    }

    public void UpdateHearts(int maxHp, int remainingHp)
    {
        hearts = new List<Image>();
        hearts.Add(heart1);
        hearts.Add(heart2);
        hearts.Add(heart3);
        if (remainingHp == 0)
        {
            // dead, game over
            Debug.Log("game over!");
        }
        else if (remainingHp > maxHp)
        {
            // something wrong
            Debug.LogError("remaining HP too high!");
        }

        for (int x = 0; x < remainingHp; x++)
        {
            hearts[x].enabled = true;
            hearts[x].color = Color.red;
        }

        for (int x = maxHp - 1; x >= remainingHp; x--)
        {
            hearts[x].enabled = true;
            hearts[x].color = Color.white;
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        foreach (FormDef formDef in JamGameController.instance.formData.Values)
        {
            
        }
    }
}
