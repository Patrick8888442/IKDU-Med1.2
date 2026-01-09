using UnityEngine;

public class StatBlock : MonoBehaviour
{
    public string characterName; 
    public int maxHP = 100;
    public int currentHP;
    public GameObject turnIndicator;
    public Alignment alignment;
    public enum Alignment {Good, Evil};
    public AttackData[] attacks;
   
    


    void Start()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int damage)
    {
        damage = Mathf.Max(damage, 0);
        currentHP -= damage;
        currentHP = Mathf.Max(currentHP, 0);

        if (currentHP == 0){
            gameObject.GetComponent<CapsuleCollider>().enabled = false;

            if (alignment ==Alignment.Evil){
                FindObjectOfType<BattleManager>().ShowVictory();
            }
        }
    }


}
