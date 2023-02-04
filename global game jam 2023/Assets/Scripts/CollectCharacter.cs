using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectCharacter : MonoBehaviour
{
    
   bool isInteracting = false;
   public bool isPickedUp = false;
   GameObject player;

   CollectingInventory instance;
   DisplayCollected display;

   float zDistance;

   TMP_Text text;
   private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player" && !isPickedUp)
        {
            //show ui maybe
            text.enabled = true;
            isInteracting = true;
           
        }
   }
   private void OnTriggerExit(Collider other) {
        if (other.tag == "Player" && !isPickedUp)
        {
            isInteracting = false;
            text.enabled = false;
        }
   }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        instance = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CollectingInventory>();
        display = GameObject.FindGameObjectWithTag("GameManager").GetComponent<DisplayCollected>();

        zDistance = transform.localScale.z;

        text = GetComponentInChildren<TMP_Text>();
        //disable to start with
        text.enabled = false;
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
         //increase by 1
        instance.SetCollected(1);
        //also change display, to show how much we have collected
        display.ChangeText(instance.GetCollected());
        
        //add to player "hand"
        this.transform.parent = player.transform;
        //move it to the player
        //this.transform.position = player.transform.position + offset;

        //this.transform.position = player.transform.position;

        //give offset, increase float value for further distance
        //offset based off of how much we currently have
        //this.transform.position += -player.transform.forward * instance.GetCollected() * 0.5f; //negative to go behind

        //what is the z scale?

        this.transform.position += new Vector3(0, 0, zDistance * instance.GetCollected() * -1.0f);
        //this.transform.localPosition = new Vector3(0, 0, this.transform.localPosition.z);

        //adjust on one axis, only on z side so they form straight line
        //this.transform.position += new Vector3(0, 0, -player.transform.forward.z * instance.GetCollected());

        isPickedUp = true;
        isInteracting = false;

    }
    

}
