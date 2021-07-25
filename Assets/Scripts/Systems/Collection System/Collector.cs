using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollectionEvent : UnityEvent<KeyValuePair<CollectableType, int>>
{

}

public class Collector : Attribute
{
    Dictionary<CollectableType, int> Collectables = new Dictionary<CollectableType, int>();

    [HideInInspector]
    public CollectionEvent event_collection = new CollectionEvent();

    private void Awake()
    {
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
        Collectables[type] = Math.Max(0, Collectables[type] + increment);

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
        foreach (CollectableType type in EnumUtil.GetValues<CollectableType>())
        {
            event_collection.Invoke(
            new KeyValuePair<CollectableType, int>(type, Collectables[type])
            );
        }

    }
}
