using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public abstract class Effect
{
    public EffectType effectType
    {
        get
        {
            return _effectType;
        }
    }

    public EffectProcessingType processingType
    {
        get
        {
            return _processingType;
        }
    }


    protected EffectType _effectType;
    protected EffectProcessingType _processingType;

    public Effect()
    {

    }


}

[System.Serializable]
public abstract class AtomicEffect : Effect
{
    protected AtomicEffect() : base()
    {
        _processingType = EffectProcessingType.Atomic;
    }
}

[System.Serializable]
public abstract class TickEffect : Effect
{
    public int interval;
    public int duration;

    protected TickEffect(int _interval, int _duration) : base()
    {
        _processingType = EffectProcessingType.Tick;

        interval = _interval;
        duration = _duration;
    }
}

[System.Serializable]
public class HealingEffect : AtomicEffect
{
    public int healAmount;

    public HealingEffect(int _healAmount) : base()
    {
        _effectType = EffectType.Heal;
        healAmount = _healAmount;
    }
}

[System.Serializable]
public class BleedingEffect : TickEffect
{
    public int damagePerTick;

    public BleedingEffect(int _damage, int _interval, int _duration) : base(_interval, _duration)
    {
        _effectType = EffectType.Bleeding;

        damagePerTick = _damage;

    }
}

[System.Serializable]
public class DamageEffect : AtomicEffect
{
    public int damage;

    public DamageEffect(int _damage) : base()
    {
        _effectType = EffectType.Damage;

        damage = _damage;
    }
}

[System.Serializable]
public class FireEffect : TickEffect
{
    public int fireDamagePerTick;

    public FireEffect(int _damage, int _interval, int _duration) : base(_interval, _duration)
    {
        _effectType = EffectType.Fire;

        fireDamagePerTick = _damage;

    }

}

[System.Serializable]
public class CollectableChangerEffect : AtomicEffect
{
    public CollectableType collectableType;
    public int amount;

    public CollectableChangerEffect(CollectableType _collectableType, int _amount) : base()
    {
        _effectType = EffectType.CollectableChanger;

        collectableType = _collectableType;
        amount = _amount;

    }
}