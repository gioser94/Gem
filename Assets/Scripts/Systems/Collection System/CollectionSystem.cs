using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionSystem : MonoBehaviour
{
    public void Collect(GameObject recipient, GameObject target)
    {
        Collector collector = recipient.GetComponent<Collector>();
        Collectable collectable = target.GetComponent<Collectable>();

        if(collector == null)
        {
            Debug.LogError("Recipient of collection " + recipient.name + " doesn't contain a collector behavior.");
            return;
        }
        if(collectable == null)
        {
            Debug.LogError("Target of collection " + target.name + " doesn't contain a collection behavior.");
            return;
        }

        collector.Collect(collectable.type, collectable.amount);


    }
}
