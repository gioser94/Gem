using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    
    public int maxLife = 200;
    
    //Life is get Only
    public int Life
    {
        get
        {
            return _life;
        }

    }

    private int _life;

    //The properties you set in editror, since it is public
    public List<PropertyType> properties = new List<PropertyType>();

    //The Behaviour instantiated on start that are part of the player
    //Each behaviour is instantiated to the player if the associated property exists
    private List<Attribute> attributes = new List<Attribute>();

    public bool bleedingAtStart;
    public BleedingEffect bleedingEffect;

    private void Awake()
    {
        _life = maxLife;


        //Properties are used so that we can instantiate the behaviors, 
        //such as collector and effectReceiver, directly for the player on start
        foreach(PropertyType p in properties)
        {
            switch (p)
            {
                case PropertyType.Collector:
                    attributes.Add(this.gameObject.AddComponent<Collector>());
                    break;
                case PropertyType.EffectSensitive:
                    attributes.Add(this.gameObject.AddComponent<EffectReceiver>());
                    break;
                case PropertyType.Killable:
                    attributes.Add(this.gameObject.AddComponent<HealthModifier>());
                    break;
                default:
                    break;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Informing the Gamemanager that I am the player. 
        //The Gamemanager forwards the info to the listeners
        GameManager.Instance.RegisterPlayer(this.gameObject);


        //Setup attributes for the listeners to know the initial state of the watched player
        foreach(Attribute att in attributes)
        {
            //If the Attribute is of type Collector, inheritance is doing implicitely
            //---> (att as Collector).Setup();
            att.Setup(); 
        }
        


        //Effect that we add on start because of gameplay requirements
        if (bleedingAtStart)
        {
            BleedingEffect tempBleedingEffect = new BleedingEffect(bleedingEffect.damagePerTick, bleedingEffect.interval, bleedingEffect.duration);
            GameManager.Instance.EffectSystem.ApplyEffect(this.gameObject, tempBleedingEffect);
        }
        
    }

    //Other classes set life of the player with this method, instead of changing the value directly (Setter Method)
    public void SetLife(int newLife)
    {
        //Value is clamped between 0 and maxLife, can't be lower or higher of these values respectively
        _life = Mathf.Clamp(newLife, 0, maxLife);
        
        if(Life <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        //TODO
    }

    public bool HasProperty(PropertyType typeToCheck)
    {
        List<PropertyType> subsetList = new List<PropertyType>();

        // Take only the properties in the array that are equal to typeToCheck and add them in the new subsetList
        foreach (PropertyType p in properties)
        {
            if(p == typeToCheck)
            {
                subsetList.Add(p);
            }
        }

        // Ask if the subsetList number of elements is greater than zero and returns the answer in bool form
        return (subsetList.Count > 0);

        //It can also be done this way
        //return properties.Where(p => p == typeToCheck).Any();

    }

    
}
