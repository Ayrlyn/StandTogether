using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMessenger : SingletonDontDestroy<EventMessenger>
{
    public event Action<Button> ButtonPressed;
    public event Action<Entity> CharacterDropped;
    public event Action<Entity> CharacterSelected;
    public event Action EndRound;
    public event Action<Entity> EntityDefeated;
    public event Action<int> PowerUpdated;
    public event Action StartRound;
    public event Action<int> TakeDamage;

    public void SendButtonPressed(Button direction)
    {
        ButtonPressed?.Invoke(direction);
    }

    public void SendCharacterDropped(Entity entity)
    {
        CharacterDropped?.Invoke(entity);
    }

    public void SendCharacterSelected(Entity entity)
    {
        CharacterSelected?.Invoke(entity);
    }

    public void SendEndRound()
    {
        EndRound?.Invoke();
    }

    public void SendEntityDefeated(Entity entity)
    {
        EntityDefeated?.Invoke(entity);
    }

    public void SendPowerUpdate(int power)
    {
        PowerUpdated?.Invoke(power);
    }

    public void SendStartRound()
    {
        StartRound?.Invoke();
    }

    public void SendTakeDamage(int damage)
    {
        TakeDamage?.Invoke(damage);
    }
}