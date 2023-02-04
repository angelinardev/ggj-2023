using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCharacter : MonoBehaviour
{
    
   bool isInteracting = false;
   public bool isPickedUp = false;
   GameObject player;
   private void OnCollisionEnter(Collision other) {
        if (other.collider.tag == "Player" && !isPickedUp)
        {
            //show ui maybe
            isInteracting = true;
           
        }
    }

    private void OnCollisionExit(Collision other) {
        if (other.collider.tag == "Player" && !isPickedUp)
        {
            isInteracting = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (isInteracting)
        {
            if (Input.GetKeyDown(KeyCode.E)) //key to "pickup"
            {
                Interact();
            }
        }   
    }

    void Interact()
    {
        //add to player "hand"
        this.transform.parent = player.transform;
        //move it to the player
        //this.transform.position = player.transform.position + offset;
        this.transform.position = player.transform.position;
        //give offset, increase float value for further distance
        this.transform.position += -player.transform.forward * 2.0f; //negative to go behind

        isPickedUp = true;
        isInteracting = false;
    }

}
