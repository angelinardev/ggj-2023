using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayCollected : MonoBehaviour
{
    public GameObject ui;
    
    public void ChangeText(int n)
    {
        ui.GetComponent<TMP_Text>().text = n.ToString();
    }

}
