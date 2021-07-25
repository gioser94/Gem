using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//GameManager uses the Singleton Pattern.
//It means that throughout the lifetime of the instance there will only be one instance of GameManager.

//To do so, we set the instance value as static, so that is not dynamically allocated as other variables
//Static means that for the whole lifetime of the program, the variable is the same for every instance of the class that defines it

//So, if the instance of the class itself is static, then there can only be one "true instance",
//and its value is assigned to the static instance variable

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;

    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public GameObject HUD_Instance_Prefab;

    private GameObject currentPlayer;

    private CollectionSystem _collectionSystem;
    private EffectSystem _effectSystem;
    private HUD_Manager _hud_manager;

    public CollectionSystem CollectionSystem
    {
        get
        {
            return _collectionSystem;
        }
    }
    
    public EffectSystem EffectSystem
    {
        get
        {
            return _effectSystem;
        }
    }


    private void Awake()
    {

        
        if(_instance != this  && _instance != null)
        {
            Destroy(this.gameObject);
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);

        //Instantiating the systems of the game
        _collectionSystem = this.gameObject.AddComponent<CollectionSystem>();
        _effectSystem = this.gameObject.AddComponent<EffectSystem>();

        //Instantiating and setting up the UI
        GameObject hud_Instance = GameObject.Instantiate(HUD_Instance_Prefab, Vector3.zero, Quaternion.identity);
        _hud_manager = hud_Instance.GetComponent<HUD_Manager>();



    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Setup systemic listeners of the player, such as HUD
    public void RegisterPlayer(GameObject newPlayer)
    {
        currentPlayer = newPlayer;
        if (_hud_manager != null)
        {
            _hud_manager.RegisterHUDTarget(currentPlayer);
        }
    }
}
