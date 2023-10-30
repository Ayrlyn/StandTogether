using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneReferences : Singleton<SceneReferences>
{
	#region serialiazable variables
	[Header("In Game")]
	[SerializeField] GameOverScreen gameOverScreen;
	[SerializeField] NewCharacterScreen newCharacterScreen;

	[Header("Data")]
	[SerializeField] List<CharacterSprite> characterSprites = new List<CharacterSprite>();
	[SerializeField] List<SmallEnemySprite> enemySprites1x1 = new List<SmallEnemySprite>();
	[SerializeField] List<TallEnemySprite> enemySprite1x2 = new List<TallEnemySprite>();
	[SerializeField] List<WideEnemySprite> enemySprites2x1 = new List<WideEnemySprite>();
	[SerializeField] List<NumberSprite> numberSprites = new List<NumberSprite>();
	[SerializeField] List<SymbolSprite> symbolSprites = new List<SymbolSprite>();
	#endregion

	#region local variables
	AudioController audioController;
	EnemySlot enemySlot;
	EventMessenger eventMessenger;
	FieldUI fieldUI;
	Game game;
	HandUI handUI;
	#endregion

	#region getters and setters
	public List<CharacterSprite> CharacterSprites { get { return characterSprites; } }
	public List<SmallEnemySprite> EnemySprites1x1 { get { return enemySprites1x1; } }
	public List<TallEnemySprite> EnemySprites1x2 { get { return enemySprite1x2; } }
	public List<WideEnemySprite> EnemySprites2x1 { get { return enemySprites2x1; } }
	public AudioController AudioController
    {
        get
        {
			if(audioController == null) { audioController = AudioController.Instance; }
			return audioController;
        }
    }
	public EnemySlot EnemySlot
    {
        get
        {
			if(enemySlot == null) { enemySlot = EnemySlot.Instance; }
			return enemySlot;
        }
    }
	public EventMessenger EventMessenger
    {
        get
        {
			if(eventMessenger == null) { eventMessenger = EventMessenger.Instance; }
			return eventMessenger;
        }
    }
	public FieldUI FieldUI
    {
        get
        {
			if(fieldUI == null) { fieldUI = FieldUI.Instance; }
			return fieldUI;
        }
    }
	public Game Game
    {
        get
        {
			if(game == null) { game = Game.Instance; }
			return game;
        }
    }
	public GameOverScreen GameOverScreen { get { return gameOverScreen; } }
	public HandUI HandUI
    {
        get
        {
			if(handUI == null) { handUI = HandUI.Instance; }
			return handUI;
        }
    }
	public NewCharacterScreen NewCharacterScreen { get { return newCharacterScreen; } }
	#endregion

	#region unity methods
	#endregion

	#region local methods
	#endregion

	#region public methods
	public Sprite GetSpriteByHealth(int health)
    {
        foreach (NumberSprite numberSprite in numberSprites)
        {
			if(numberSprite.Integer == health) { return numberSprite.Sprite; }
		}
		Debug.LogError("How did we get here?");
		return null;
	}

	public Sprite GetSrpiteBySymbol(Symbol symbol)
    {
        foreach (SymbolSprite symbolSprite in symbolSprites)
        {
			if(symbolSprite.Symbol == symbol) { return symbolSprite.Sprite; }
        }
		Debug.LogError("How did we get here?");
		return null;
    }
	#endregion

	#region coroutines
	#endregion
}

[Serializable]
public class CharacterSprite
{
	[SerializeField] Sprite forwardSprite;
	[SerializeField] Sprite readySprite;
	[SerializeField] Sprite attackSprite;
	[SerializeField] Sprite hurtSprite;

	public Sprite ForwardSprite { get { return forwardSprite; } }
	public Sprite ReadySprite { get { return readySprite; } }
	public Sprite AttackSprite { get { return attackSprite; } }
	public Sprite HurtSprite { get { return hurtSprite; } }
}

[Serializable]
public class DifficultyCurve
{
	[SerializeField] int round;
	[SerializeField] int health;

	public int Round { get { return round; } }
	public int Health { get { return health; } }
}

[Serializable]
public class NumberSprite
{
	[SerializeField] string name;
	[SerializeField] int integer;
	[SerializeField] Sprite sprite;

	public int Integer { get { return integer; } }
	public string Name { get { return name; } }
	public Sprite Sprite { get { return sprite; } }
}

[Serializable]
public class SmallEnemySprite
{
	[SerializeField] string name;
	[SerializeField] AnimationClip sprite;

	public string Name { get { return name; } }
	public AnimationClip Sprite { get { return sprite; } }
}

[Serializable]
public class SuperEffective
{
	[SerializeField] Symbol symbol;
	[SerializeField] Symbol strength1;
	[SerializeField] Symbol strength2;

	public Symbol Symbol { get { return symbol; } }
	public Symbol Strength1 { get { return strength1; } }
	public Symbol Strength2 { get { return strength2; } }
}

[Serializable]
public class SymbolSprite
{
	[SerializeField] string name;
	[SerializeField] Sprite sprite;
	[SerializeField] Symbol symbol;

	public string Name { get { return name; } }
	public Sprite Sprite { get { return sprite; } }
	public Symbol Symbol { get { return symbol; } }
}

[Serializable]
public class TallEnemySprite
{
	[SerializeField] string name;
	[SerializeField] AnimationClip bottomSprite;
	[SerializeField] AnimationClip topSprite;

	public AnimationClip BottomSprite { get { return bottomSprite; } }
	public string Name { get { return name; } }
	public AnimationClip TopSprite { get { return topSprite; } }
}


[Serializable]
public class WideEnemySprite
{
	[SerializeField] string name;
	[SerializeField] AnimationClip leftSprite;
	[SerializeField] AnimationClip rightSprite;

	public AnimationClip LeftSpright { get { return leftSprite; } }
	public string Name { get { return name; } }
	public AnimationClip RightSprite { get { return rightSprite; } }
}
