using UnityEngine;
using UnityEngine.UI;

public class AttackButton : MonoBehaviour
{
    public Image icon;
    int attackIndex;
    BattleManager battleManager;

    public void Setup(Sprite sprite, int index, BattleManager manager)
    {
        icon.sprite = sprite;
        attackIndex = index;
        battleManager = manager;

        GetComponent<Button>().onClick.RemoveAllListeners();
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        battleManager.PlayerAttack(attackIndex);
    }
}

