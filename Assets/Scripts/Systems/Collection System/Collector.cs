using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class CollectionEvent : UnityEvent<KeyValuePair<CollectableType, int>> { }

public class Collector : Attribute
{
    //Dictionary of all the collectibles collected so far, categoryzed by collectible type
    Dictionary<CollectableType, int> Collectables = new Dictionary<CollectableType, int>();

    //The event variable called everytime something is collected
    [HideInInspector]
    public CollectionEvent event_collection = new CollectionEvent();

    private void Awake()
    {
        //Setup the dictionary, adding an entry for each collectible type, with initial amount at zero
        foreach (CollectableType type in EnumUtil.GetValues<CollectableType>())
        {
            Collectables.Add(type, 0);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Collect(CollectableType type, int increment)
    {
        //Update the dictionary, the collectible can't go below zero
        Collectables[type] = Math.Max(0, Collectables[type] + increment);

        //The event is called to warn the listeners, such as HUD to update accordingly
        event_collection.Invoke(
            new KeyValuePair<CollectableType, int>(type, Collectables[type])
            );

    }

    public int GetGemCount()
    {
        return Collectables[CollectableType.Gem];
    }

    public override void Setup()
    {
        //On Setup, warns for each collectible type the amount at start, zero by default
        foreach (CollectableType type in EnumUtil.GetValues<CollectableType>())
        {
            event_collection.Invoke(
            new KeyValuePair<CollectableType, int>(type, Collectables[type])
            );
        }

    }
}
