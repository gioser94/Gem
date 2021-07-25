using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionSystem : MonoBehaviour
{
    public bool Collect(GameObject recipient, GameObject target)
    {

        //Finds the relative component for collectable and collector
        Collector collector = recipient.GetComponent<Collector>();
        Collectable collectable = target.GetComponent<Collectable>();


        //If one of them doesn't exist, it stops the execution with an error
        if(collector == null)
        {
            Debug.LogError("Recipient of collection " + recipient.name + " doesn't contain a collector behavior.");
            return false;
        }
        if(collectable == null)
        {
            Debug.LogError("Target of collection " + target.name + " doesn't contain a collection behavior.");
            return false;
        }

        //If the behavior exist, then the collection is considered valid and applied
        collector.Collect(collectable.type, collectable.amount);
        return true;


    }
}
