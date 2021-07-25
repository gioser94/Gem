using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{

    public HealingEffect healingEffect;
    EffectSystem effectSystem;

    // Start is called before the first frame update
    void Start()
    {
        healingEffect = new HealingEffect(healingEffect.healAmount);
        effectSystem = GameManager.Instance.EffectSystem;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        effectSystem.ApplyEffect(other.gameObject, healingEffect);
        Destroy(this.gameObject);
    }


}
