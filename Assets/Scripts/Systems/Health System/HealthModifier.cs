using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthChangedEvent : UnityEvent<int>
{

}

public class HealthModifier : Attribute
{

    [HideInInspector]
    public HealthChangedEvent event_HealthChanged = new HealthChangedEvent();

    public void Damage(int damageValue)
    {
        Stats thisStats = this.GetComponent<Stats>();
        thisStats.SetLife(thisStats.Life - damageValue);

        event_HealthChanged.Invoke(thisStats.Life);
    }

    public void Heal(int healingAmount)
    {
        Stats thisStats = this.GetComponent<Stats>();
        thisStats.SetLife(thisStats.Life + healingAmount);

        event_HealthChanged.Invoke(thisStats.Life);
    }

    public override void Setup()
    {
        event_HealthChanged.Invoke(this.GetComponent<Stats>().Life);
    }
}
