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
    public TextMeshProUGUI manaValue;
    public TextMeshProUGUI formName;
    public bool initialized = false;
    public void Initialize(FormDef form)
    {
        this.initialized = true;
        this.form = form;
        squibby.sprite = form.formSprite;
        
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
