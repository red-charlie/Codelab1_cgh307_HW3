using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemScript : MonoBehaviour
{
  
    public string itemName;
  
    public Text itemNameText;
    
    // Start is called before the first frame update
    void Start()
    {
        itemNameText.text = itemName;
    }

    




}
