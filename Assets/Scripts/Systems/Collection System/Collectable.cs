using UnityEngine;


//This behavior should be assigned to all the objects that can be collected. It represent the collectibility of an object
public class Collectable : MonoBehaviour
{
    //Properties of the collectible to be set in editor.
    //For example, a type = Gem and amount = 5
    public CollectableType type;
    public int amount;

    private CollectionSystem collectionSystem;

    // Start is called before the first frame update
    void Start()
    {
        collectionSystem = GameManager.Instance.CollectionSystem;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //The collectible's collider is entered by someone. the collectible asks the CollectionSystem
    //to be collected by the Collector behavior of this object
    private void OnTriggerEnter(Collider other)
    {
        //Asks the Collection System for being collected by the collider GameObject
        if(collectionSystem.Collect(other.gameObject, this.gameObject))
        {
            //If the collection is successful, the collector Destroys itself
            Destroy(this.gameObject);
        }


    }


}