using UnityEngine;

[CreateAssetMenu(
    fileName = "NewAttackData",
    menuName = "Combat/Attack Data"
)]
public class AttackData : ScriptableObject
{
    public string attackName;
    public Sprite icon;
    public GameObject projectilePrefab;
    public int damage;
    public AttackType attackType;
    public enum AttackType {Magic, Ranged, Melee}
}