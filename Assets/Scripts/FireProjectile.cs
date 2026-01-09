using UnityEngine;

public class FireProjectile : ProjectileBase
{
    protected override Vector3 GetTargetPosition(){
        return target.transform.position + Vector3.up * 1.5f;
    }
}
