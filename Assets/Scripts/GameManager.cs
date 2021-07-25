using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject HUD_Instance_Prefab;

    private static GameManager instance = null;

    private GameObject currentPlayer;


    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

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

       
        if(instance != this  && instance != null)
        {
            Destroy(this.gameObject);
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);

        _collectionSystem = this.gameObject.AddComponent<CollectionSystem>();
        _effectSystem = this.gameObject.AddComponent<EffectSystem>();

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

    public void RegisterPlayer(GameObject newPlayer)
    {
        currentPlayer = newPlayer;
        if (_hud_manager != null)
        {
            _hud_manager.RegisterHUDTarget(currentPlayer);
        }
    }
}
