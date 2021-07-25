using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_Manager : MonoBehaviour
{

    public Text LifeText;
    public Text GemText;

    private GameObject currentTarget;

        // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RegisterHUDTarget(GameObject target)
    {

        currentTarget = target;
        currentTarget.GetComponent<Collector>().event_collection.AddListener(UpdateCollectedGem);
        currentTarget.GetComponent<HealthModifier>().event_HealthChanged.AddListener(UpdateCurrentLife);
    }

    private void UpdateCollectedGem(KeyValuePair<CollectableType, int> collectorKeyValuePair)
    {
        if(collectorKeyValuePair.Key == CollectableType.Gem)
        {
            GemText.text = ("GEM : " + collectorKeyValuePair.Value);
        }
        
    }

    private void UpdateCurrentLife(int newLife)
    {
        LifeText.text = ("LIFE : " + newLife);
    }
}
