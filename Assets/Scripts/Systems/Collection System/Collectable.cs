using UnityEngine;



public class Collectable : MonoBehaviour
{

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

    private void OnTriggerEnter(Collider other)
    {
        collectionSystem.Collect(other.gameObject, this.gameObject);
        Destroy(this.gameObject);
    }


}