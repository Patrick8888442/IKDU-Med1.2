using UnityEngine;
using System.Collections;

public abstract class ProjectileBase : MonoBehaviour
{
  public float speed = 5f;
  protected StatBlock target;
  protected int damage;

  public virtual void Initialize(StatBlock target, int damage){
    this.target = target;
    this.damage = damage;
    StartCoroutine(MoveToTarget());
  }

  protected virtual Vector3 GetTargetPosition(){
    return target.transform.position;
  }

  protected virtual IEnumerator MoveToTarget(){

    while(target != null){
        Vector3 targetPos = GetTargetPosition();
 
    if (Vector3.Distance(transform.position, targetPos) < 0.1f)
      break;

      transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        yield return null;
    }
       OnHit();
  }

  protected virtual void OnHit(){
    if (target != null){
        target.TakeDamage(damage);
    }

    Destroy(gameObject);
  }
}
