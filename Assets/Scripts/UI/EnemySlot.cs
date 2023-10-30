using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySlot : Singleton<EnemySlot>
{
	#region serialiazable variables
	[SerializeField] Animator enemyFloat;
	[SerializeField] Animator enemySmall;
	[SerializeField] Animator enemyTallBottom;
	[SerializeField] Animator enemyTallTop;
	[SerializeField] Animator enemyWideLeft;
	[SerializeField] Animator enemyWideRight;
    [SerializeField] Image healthHundreds;
    [SerializeField] Image healthTens;
    [SerializeField] Image healthUnits;
    [SerializeField] Image symbol;
	#endregion

	#region local variables
	List<GameObject> animators = new List<GameObject>();

    SceneReferences sceneReferences;
    #endregion

    #region getters and setters
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
        animators.Add(enemyFloat.gameObject);
        animators.Add(enemySmall.gameObject);
        animators.Add(enemyTallBottom.gameObject);
        animators.Add(enemyTallTop.gameObject);
        animators.Add(enemyWideLeft.gameObject);
        animators.Add(enemyWideRight.gameObject);

        DeactivateAnimators();
    }
    #endregion

    #region local methods
    void ActivateFloatEnemy()
    {
        enemyFloat.gameObject.SetActive(true);
        enemyFloat.SetTrigger(SceneReferences.EnemySprites1x1.GetRandomElement().Name);
    }

    void ActivateSmallEnemy()
    {
        enemySmall.gameObject.SetActive(true);
        enemySmall.SetTrigger(SceneReferences.EnemySprites1x1.GetRandomElement().Name);
    }

    void ActivateTallEnemy()
    {
        string enemyTall = sceneReferences.EnemySprites1x2.GetRandomElement().Name;
        enemyTallBottom.gameObject.SetActive(true);
        enemyTallBottom.SetTrigger($"{enemyTall}Lower");
        enemyTallTop.gameObject.SetActive(true);
        enemyTallTop.SetTrigger($"{enemyTall}Upper");
    }

    void ActivateWideEnemy()
    {
        string enemyWide = SceneReferences.EnemySprites2x1.GetRandomElement().Name;
        enemyWideLeft.gameObject.SetActive(true);
        enemyWideLeft.SetTrigger($"{enemyWide}Left");
        enemyWideRight.gameObject.SetActive(true);
        enemyWideRight.SetTrigger($"{enemyWide}Right");
    }

    void DeactivateAnimators()
    {
        foreach(GameObject animator in animators) { animator.SetActive(false); }
    }
    #endregion

    #region public methods
    public void DisplayHealth(Entity entity)
    {
        int health = 0;
        if(entity != null) {health = entity.CurrentHealth; }

        if (health < 100) { healthHundreds.gameObject.SetActive(false); }
        else
        {
            healthHundreds.gameObject.SetActive(true);
            healthHundreds.sprite = SceneReferences.GetSpriteByHealth(health / 100);
        }

        if (health < 10) { healthTens.gameObject.SetActive(false); }
        else
        {
            healthTens.gameObject.SetActive(true);
            healthTens.sprite = SceneReferences.GetSpriteByHealth((health % 100) / 10);
        }

        healthUnits.sprite = SceneReferences.GetSpriteByHealth(health % 10);
    }

    public void SetEncounterSprites(Entity entity, int round)
    {
        DeactivateAnimators();
        DisplayHealth(entity);
        if (entity == null) { return; }

        symbol.sprite = SceneReferences.GetSrpiteBySymbol(entity.Symbol);
        switch (round)
        {
            case 1:
                ActivateSmallEnemy();
                break;
            case 2:
                ActivateSmallEnemy();
                ActivateFloatEnemy();
                break;
            case 3:
                ActivateWideEnemy();
                break;
            case 4:
                ActivateWideEnemy();
                ActivateSmallEnemy();
                break;
            case 5:
                ActivateWideEnemy();
                ActivateSmallEnemy();
                ActivateFloatEnemy();
                break;
            case 6:
                ActivateTallEnemy();
                break;
            case 7:
                ActivateTallEnemy();
                ActivateSmallEnemy();
                break;
            case 8:
                ActivateTallEnemy();
                ActivateSmallEnemy();
                ActivateFloatEnemy();
                break;
            case 9:
                ActivateTallEnemy();
                ActivateWideEnemy();
                break;
            case 10:
                ActivateFloatEnemy();
                ActivateSmallEnemy();
                ActivateTallEnemy();
                ActivateWideEnemy();
                break;
            default:
                break;
        }
    }
    #endregion

    #region coroutines
    #endregion
}
