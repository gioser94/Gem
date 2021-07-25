using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectReceiver : Attribute
{

    public struct effectTickData
    {
        public int Counter;
        public float elapsedTotal;
    };

    private Dictionary<EffectProcessingType, List<Effect>> EffectDictionary = new Dictionary<EffectProcessingType, List<Effect>>();
    private Dictionary<Effect, effectTickData> elapsedSecondsForEffect = new Dictionary<Effect, effectTickData>();

    private void Awake()
    {
        foreach (EffectProcessingType type in EnumUtil.GetValues<EffectProcessingType>())
        {
            EffectDictionary.Add(type, new List<Effect>());
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        List<Effect> effectsToRemove = new List<Effect>();

        foreach(Effect e in EffectDictionary[EffectProcessingType.Tick])
        {

            
            effectTickData newTickData = elapsedSecondsForEffect[e];
            newTickData.elapsedTotal += Time.deltaTime;

            if (newTickData.elapsedTotal >= (e as TickEffect).duration && (e as TickEffect).duration != -1)
            {
                effectsToRemove.Add(e);
                continue;
            }

            if (Mathf.FloorToInt(newTickData.elapsedTotal / (e as TickEffect).interval) > newTickData.Counter)
            {
                newTickData.Counter++;
                ApplyTickEffect(e);
            }

            elapsedSecondsForEffect[e] = newTickData;

            

        }

        foreach (Effect e in effectsToRemove)
        {
            EffectDictionary[EffectProcessingType.Tick].Remove(e);
        }
    }

    private void ApplyTickEffect(Effect e)
    {
        switch (e.effectType)
        {
            case EffectType.Bleeding:
                this.AddEffect(new DamageEffect((e as BleedingEffect).damagePerTick));
                break;

            default:
                break;
        }
    }

    void ConsumeEffects()
    {
        foreach(Effect e in EffectDictionary[EffectProcessingType.Atomic])
        {
            if(e.effectType == EffectType.Damage)
            {
                DamageEffect newDamageEffect = e as DamageEffect;
                this.GetComponent<HealthModifier>().Damage(newDamageEffect.damage);
            }

            if (e.effectType == EffectType.Heal)
            {
                HealingEffect newDamageEffect = e as HealingEffect;
                this.GetComponent<HealthModifier>().Heal(newDamageEffect.healAmount);
            }

            else if(e.effectType == EffectType.CollectableChanger)
            {
                CollectableChangerEffect newCollectableChangerEffect = e as CollectableChangerEffect;
                this.GetComponent<Collector>().Collect(newCollectableChangerEffect.collectableType, newCollectableChangerEffect.amount);
            }
        }

        EffectDictionary[EffectProcessingType.Atomic].Clear();
    }

    public void AddEffect(Effect newEffect)
    {
        EffectProcessingType processingType;
        processingType = newEffect.processingType;

        EffectDictionary[processingType].Add(newEffect);

        if(processingType == EffectProcessingType.Tick)
        {
            elapsedSecondsForEffect.Add(newEffect, new effectTickData());
            
        }

        ConsumeEffects();
    }

    public void RemoveEffect(Effect effectToRemove)
    {
        EffectDictionary[EffectProcessingType.Tick].Remove(effectToRemove);
        elapsedSecondsForEffect.Remove(effectToRemove);
    }
    
    public override void Setup()
    {

    }

}
