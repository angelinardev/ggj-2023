using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCondition : MonoBehaviour
{
    public int max_amount_needed;

    CollectingInventory instance;
    
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player")
        {
            //simple for now, could maybe make it more complex
            if (instance.GetCollected() == max_amount_needed)
            {
                //win? win screen?
                Debug.Log("You win!");

                //placeholder for now, don't know what it will be
                SceneManager.LoadScene("EndScreen");

            }
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
         instance = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CollectingInventory>();
    }

}
