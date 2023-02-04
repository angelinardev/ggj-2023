using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelDistance : MonoBehaviour
{
    public GameObject disDisplay;
    public int disRun;
    public bool addingDis = false;
    public float disDelay = 0.35f;

    // Update is called once per frame
    void Update()
    {
        if (!addingDis)
        {
            addingDis = true;
            StartCoroutine(AddingDis());
        }
    }

    IEnumerator AddingDis()
    {
        disRun +=1;
        disDisplay.GetComponent<TMP_Text>().text = disRun.ToString();
        yield return new WaitForSeconds(disDelay);
        addingDis = false;
    }
}
