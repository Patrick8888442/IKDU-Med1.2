using UnityEngine;
using System.Collections;

public class BattleManager : MonoBehaviour
{
    public GameObject victoryCanvas;
    public StatBlock[] statBlocks;
    private int activeCharacterIndex = 0;
    private float turnDelay = 4f;
    bool waitingForPlayerInput = false;
    public Transform attackButtonParent;
    public GameObject attackButtonPrefab;
    public GameObject actionMenu;

    void Start()
    {
        for (int i = 0; i < statBlocks.Length; i++){
            if (statBlocks[i].turnIndicator != null){
                statBlocks[i].turnIndicator.SetActive(i == activeCharacterIndex);
            }
        }
        StartCoroutine(TurnLoop());
    }
    

    public StatBlock GetActiveCharacter()
    {
        return statBlocks[activeCharacterIndex];
    }

 void BuildAttackUI(StatBlock character)
{
    foreach (Transform child in attackButtonParent)
        Destroy(child.gameObject);

    for (int i = 0; i < character.attacks.Length; i++)
    {
        GameObject btnObj = Instantiate(
            attackButtonPrefab,
            attackButtonParent
        );

        AttackButton btn = btnObj.GetComponent<AttackButton>();
        btn.Setup(character.attacks[i].icon, i, this);
    }
}

    public void ShowVictory(){
        victoryCanvas.SetActive(true);
    }

    IEnumerator TurnLoop(){
        while(true)
        {
            StatBlock attacker = GetActiveCharacter();
            if (attacker.alignment == StatBlock.Alignment.Good){
                waitingForPlayerInput = true;

                actionMenu.SetActive(true);
                BuildAttackUI(attacker);

                while (waitingForPlayerInput){
                    yield return null;
                }
                actionMenu.SetActive(false);
            } else {
            
            yield return new WaitForSeconds(turnDelay);
            yield return StartCoroutine(ExecuteAttack(attacker));
            }
            
            NextMember(); 
          }
        } 


    public void PlayerAttack(int attackIndex){
      if (!waitingForPlayerInput)
        return;

    StatBlock attacker = GetActiveCharacter();
    AttackData attack = attacker.attacks[attackIndex];

    StatBlock target = ChooseTarget(attacker);
    if (target == null)
        return;

    if (attack.projectilePrefab != null)
    {
        GameObject proj = Instantiate(
            attack.projectilePrefab,
            attacker.transform.position,
            Quaternion.identity
        );

        ProjectileBase projectile = proj.GetComponent<ProjectileBase>();
        projectile.Initialize(target, attack.damage);
    }
    else
    {
        target.TakeDamage(attack.damage);
    }

    waitingForPlayerInput = false;
 }   


    public void NextMember(){
        if (statBlocks[activeCharacterIndex].turnIndicator != null){
            statBlocks[activeCharacterIndex].turnIndicator.SetActive(false);
        }

        do {
            
        activeCharacterIndex++;
        if (activeCharacterIndex >= statBlocks.Length){
            activeCharacterIndex = 0;
            }
        }
        while (statBlocks[activeCharacterIndex].currentHP <= 0);

            if (statBlocks[activeCharacterIndex].turnIndicator != null){
            statBlocks[activeCharacterIndex].turnIndicator.SetActive(true);
        }
    }
//
    StatBlock ChooseTarget(StatBlock attacker){
        StatBlock bestTarget = null;
        int lowestHP = int.MaxValue;

        foreach (StatBlock character in statBlocks){
            if (character.currentHP <= 0){
                continue;
            }
            if (character.alignment == attacker.alignment){
                continue;
            }
            if (character.currentHP < lowestHP){
                lowestHP = character.currentHP;
                bestTarget = character;
            }
        }
        return bestTarget;
    }


    IEnumerator ExecuteAttack(StatBlock attacker){
        if (attacker.attacks == null || attacker.attacks.Length == 0){
            Debug.LogWarning(attacker.characterName + " has no attacks!");
            yield break;
        }

        AttackData attack = attacker.attacks[0];
        StatBlock target = ChooseTarget(attacker);

        if (target == null){
            yield break;
        }
        
        Animator animator = attacker.GetComponent<Animator>();
        if (animator != null){
            animator.SetTrigger("Slam");
        }

        yield return new WaitForSeconds(0.4f);

        if (attack.projectilePrefab != null){
            GameObject proj = Instantiate(attack.projectilePrefab, attacker.transform.position, Quaternion.identity);
            proj.GetComponent<ProjectileBase>().Initialize(target, attack.damage);
        } else {
            target.TakeDamage(attack.damage);
        }
        Debug.Log(attacker.characterName + " uses " + attack.attackName);
    }


    StatBlock FindAliveCharacterByName(string name){
        foreach (StatBlock character in statBlocks){
            if (character.characterName == name && character.currentHP > 0){
                return character;
            }
        }
        return null;
    }

//
}

