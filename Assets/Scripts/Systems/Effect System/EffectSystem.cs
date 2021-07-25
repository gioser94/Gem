using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSystem : MonoBehaviour
{

    public void ApplyEffect(GameObject target, Effect effect)
    {
        
        if (!target.GetComponent<Stats>().HasProperty(PropertyType.EffectSensitive))
        {
            Debug.LogError("Target " + target.name + " can't process effects.");
        }

        target.GetComponent<EffectReceiver>().AddEffect(effect);


    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
