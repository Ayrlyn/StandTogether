using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity
{
    #region serialiazable variables
    #endregion

    #region local variables
    int currentHealth;
    int health;
    int sprite;
    Symbol symbol;
    Tag tag;

    SceneReferences sceneReferences;
    #endregion

    #region getters and setters
    public int CurrentHealth { get { return currentHealth; } }
    public int Health { get { return health; } }
    public int Sprite { get { return sprite; } }
    public Symbol Symbol { get { return symbol; } }
    public Tag Tag { get { return tag; } }

    SceneReferences SceneReferences
    {
        get
        {
            if (sceneReferences == null) { sceneReferences = SceneReferences.Instance; }
            return sceneReferences;
        }
    }
    #endregion

    #region constructors
    public Entity(int health, Symbol symbol, Tag tag)
    {
        this.health = health;
        this.currentHealth = health;
        this.symbol = symbol;
        this.tag = tag;
        switch (tag)
        {
            case Tag.Enemy:
                break;
            case Tag.Player:
                sprite = Random.Range(0, SceneReferences.CharacterSprites.Count);
                break;
            default:
                break;
        }
    }
    #endregion

    #region local methods
    #endregion

    #region public methods
    public int TakeDamage(int damage)
    {
        currentHealth -= damage;
        if(currentHealth > 9 && damage < 0) { currentHealth = 9; }
        if(currentHealth < 0) { currentHealth = 0; }
        return currentHealth;
    }
    #endregion
}
