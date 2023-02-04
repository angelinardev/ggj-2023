using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectingInventory : MonoBehaviour
{
    
    int number_collected = 0;

   

    public int GetCollected()
    {
        return number_collected;
    }
    public void SetCollected(int n)
    {
        number_collected += n;
    }
    

}
