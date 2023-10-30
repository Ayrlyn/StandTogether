using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Game : Singleton<Game>
{
    #region serialiazable variables
    [SerializeField] List<Slot> frontSlots = new List<Slot>();
    [SerializeField] List<Slot> rearSlots = new List<Slot>();
    [SerializeField] Slot initialSelectedSlot;
    [SerializeField] List<DifficultyCurve> difficultyCurve = new List<DifficultyCurve>();
    [SerializeField] List<SuperEffective> symbolAdvantageTable = new List<SuperEffective>();
    #endregion

    #region local variables
    Entity enemyCharacter;
    List<Entity> fieldPlayerCharacters = new List<Entity>();
    List<Entity> playerCharacters = new List<Entity>();
    int power = 0;
    int round = 0;
    bool roundStarted = false;
    Entity selectedCharacter;

    SceneReferences sceneReferences;
    #endregion

    #region getters and setters
    public int Power { get { return power; } }
    public bool RoundStarted { get { return roundStarted; } }
    public Entity SelectedCharacter { get { return selectedCharacter; } }

    SceneReferences SceneReferences
    {
        get
        {
            if (sceneReferences == null) { sceneReferences = SceneReferences.Instance; }
            return sceneReferences;
        }
    }
    #endregion

    #region unity methods
    void Start()
    {
        RegisterEvents();
        NewGame();
    }

    void OnDestroy()
    {
        UnregisterEvents();
    }
    #endregion

    #region local methods
    void FirstRound()
    {
        enemyCharacter = new Entity(2, Symbol.Circle, Tag.Enemy);
        round = 1;
        SceneReferences.EnemySlot.SetEncounterSprites(enemyCharacter, round);
    }

    void GameOver(bool victory)
    {
        SceneReferences.HandUI.gameObject.SetActive(false);
        SceneReferences.FieldUI.gameObject.SetActive(false);
        SceneReferences.GameOverScreen.ShowGameOverScreen(victory);
        SceneReferences.AudioController.PlayGameOver(victory);
    }

    Symbol[] GetSymbolWeakness(Symbol symbol)
    {
        Symbol[] weaknesses = new Symbol[2];
        foreach (SuperEffective value in symbolAdvantageTable)
        {
            if (value.Strength1 == symbol) { weaknesses[0] = value.Symbol; }
            if (value.Strength2 == symbol) { weaknesses[1] = value.Symbol; }
        }
        return weaknesses;
    }

    Entity NewPlayerCharacter()
    {
        int health = Random.Range(2, 6);
        Symbol symbol = (Symbol)Random.Range(1, 5);
        return new Entity(health, symbol, Tag.Player);
    }

    void NextRound()
    {
        SceneReferences.HandUI.HealAll();
        SceneReferences.HandUI.ClearAll();
        initialSelectedSlot.MarkHighlightActive();

        round++;
        int health = 0;
        foreach (DifficultyCurve entry in difficultyCurve)
        {
            if(entry.Round == round)
            {
                health = entry.Health;
                break;
            }
        }
        Symbol symbol = (Symbol)Random.Range(1, 5);
        enemyCharacter = new Entity(health, symbol, Tag.Enemy);
        SceneReferences.EnemySlot.SetEncounterSprites(enemyCharacter, round);
        foreach (Entity entity in playerCharacters)
        {
            SceneReferences.HandUI.AssignToFirstAvailableSlot(entity);
        }
        StartCoroutine(DelayedPowerUpdate());
    }

    void OnCharacterDropped(Entity entity)
    {
        if(SelectedCharacter == entity)
        {
            selectedCharacter = null;
        }
        StartCoroutine(DelayedPowerUpdate());
    }

    void OnCharacterSelected(Entity entity)
    {
        selectedCharacter = entity;
    }

    void OnEntityDefeated(Entity entity)
    {
        fieldPlayerCharacters.Remove(entity);
        StartCoroutine(DelayedPowerUpdate());
    }

    void OnRoundStarted()
    {
        if (roundStarted) { return; }

        StartCoroutine(BeginCombat());
    }

    void RegisterEvents()
    {
        SceneReferences.EventMessenger.CharacterDropped += OnCharacterDropped;
        SceneReferences.EventMessenger.CharacterSelected += OnCharacterSelected;
        SceneReferences.EventMessenger.StartRound += OnRoundStarted;
    }

    void UnregisterEvents()
    {
        try
        {
            SceneReferences.EventMessenger.CharacterDropped -= OnCharacterDropped;
            SceneReferences.EventMessenger.CharacterSelected -= OnCharacterSelected;
            SceneReferences.EventMessenger.StartRound -= OnRoundStarted;
        }
        catch(Exception e)
        {
            Debug.LogWarning(e);
        }
    }
    #endregion

    #region public methods
    public bool FrontLineExists()
    {
        foreach (Slot slot in frontSlots)
        {
            if(slot.Entity != null) { return true; }
        }
        return false;
    }

    public void NextRound(Entity entity)
    {
        playerCharacters.Add(entity);

        NextRound();
    }

    public void NewGame()
    {
        fieldPlayerCharacters.Clear();
        playerCharacters.Clear();
        SceneReferences.HandUI.ClearAll();
        StartCoroutine(DelayedPowerUpdate());

        initialSelectedSlot.MarkHighlightActive();
        Entity firstCharacter = new Entity(3, Symbol.Circle, Tag.Player);
        playerCharacters.Add(firstCharacter);
        SceneReferences.HandUI.AssignToFirstAvailableSlot(firstCharacter);

        FirstRound();
    }
    #endregion

    #region coroutines
    IEnumerator BeginCombat()
    {
        roundStarted = true;
        while (!fieldPlayerCharacters.IsEmpty() && enemyCharacter != null)
        {
            StartCoroutine(DelayedPowerUpdate());
            yield return new WaitForSecondsRealtime(0.5f); if (FrontLineExists())
            {
                foreach (Slot slot in frontSlots)
                {
                    if (slot.Entity != null) { slot.Entity.TakeDamage(1); }
                }
            }
            else
            {
                foreach (Slot slot in rearSlots)
                {
                    if (slot.Entity != null) { slot.Entity.TakeDamage(1); }
                }
            }
            SceneReferences.EventMessenger.SendTakeDamage(Power);
            fieldPlayerCharacters.RemoveAll(item => item.Health <= 0);
            enemyCharacter.TakeDamage(Power);
            SceneReferences.EnemySlot.DisplayHealth(enemyCharacter);

            StartCoroutine(DelayedPowerUpdate());
            yield return new WaitForSecondsRealtime(1f);

            if(enemyCharacter.CurrentHealth <= 0)
            {
                enemyCharacter = null;
                SceneReferences.EnemySlot.SetEncounterSprites(enemyCharacter, round);
            }
        }
        roundStarted = false;
        if(enemyCharacter == null)
        {
            SceneReferences.EventMessenger.SendEndRound();

            if(round >= 10)
            {
                GameOver(true);
            }
            else
            {
                if (playerCharacters.Count < 5)
                {
                    SceneReferences.NewCharacterScreen.SetCharacterChoices(NewPlayerCharacter(), NewPlayerCharacter());
                    SceneReferences.FieldUI.gameObject.SetActive(false);
                    SceneReferences.HandUI.gameObject.SetActive(false);
                }
                else { NextRound(); }
            }
        }
        else if (fieldPlayerCharacters.IsEmpty())
        {
            GameOver(false);
        }
    }

    public IEnumerator DelayedPowerUpdate()
    {
        yield return new WaitForEndOfFrame();

        HashSet<Symbol> symbols = new HashSet<Symbol>();
        List<Entity> entities = new List<Entity>();

        foreach (Slot slot in frontSlots)
        {
            if (slot.Entity == null) { continue; }

            symbols.Add(slot.Entity.Symbol);
            entities.Add(slot.Entity);
        }
        foreach (Slot slot in rearSlots)
        {
            if (slot.Entity == null) { continue; }

            symbols.Add(slot.Entity.Symbol);
            entities.Add(slot.Entity);
        }

        fieldPlayerCharacters = entities;
        power = symbols.Count * entities.Count;
        Symbol[] weaknesses = GetSymbolWeakness(enemyCharacter.Symbol);
        if(symbols.Contains(weaknesses[0]) || symbols.Contains(weaknesses[1])) { power *= 2; }

        SceneReferences.EventMessenger.SendPowerUpdate(power);
    }
    #endregion
}
