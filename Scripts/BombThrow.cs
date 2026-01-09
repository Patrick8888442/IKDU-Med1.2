using UnityEngine;
using System.Collections;

public class BombThrow : ProjectileBase
{
    public float arcHeight = 1.2f;
    public GameObject explosiveVFXPrefab;

 protected override IEnumerator MoveToTarget(){
    Vector3 startPos = transform.position;
    Vector3 endPos = GetFeetPosition();

    float distance = Vector3.Distance(startPos, endPos);
    float travelTime = distance / speed;
    float elapsed = 0f;

    while(elapsed < travelTime){
        float t = elapsed / travelTime;

        Vector3 pos = Vector3.Lerp(startPos, endPos, t);

        float height = Mathf.Sin(t * Mathf.PI) * arcHeight;
        pos.y += height;

        if (pos.y < endPos.y){
            pos.y = endPos.y;
        }

        transform.position = pos;

        elapsed += Time.deltaTime;
        yield return null;
    }

   transform.position = endPos;
   OnHit();
  }



 protected override void OnHit(){
    if (explosiveVFXPrefab == null){
        Debug.LogError("Explosion VFX Prefab is not assigned!");
        base.OnHit();
        return;
    }

    GameObject explosion = Instantiate(explosiveVFXPrefab, transform.position, Quaternion.identity);
    ParticleSystem ps = explosion.GetComponentInChildren<ParticleSystem>();

    if (ps != null){
        ps.Play();

        StartCoroutine(StopAndDestroy(ps));
    } else {
        Debug.LogWarning("Explosion prefab has no ParticleSystem!");
        Destroy(explosion, 2f);
    }
    
    Debug.Log("Bombaclatt!");
    base.OnHit();
 }

   Vector3 GetFeetPosition(){
    Collider col =target.GetComponent<Collider>();
    if (col != null){
        return new Vector3(col.bounds.center.x+1f, col.bounds.min.y+0.05f, col.bounds.center.z-1f);
    }

    return target.transform.position;
  }

 IEnumerator StopAndDestroy(ParticleSystem rootPs){
    yield return new WaitForSeconds(0.5f);

    ParticleSystem[] systems = rootPs.GetComponentsInChildren<ParticleSystem>();

    foreach (ParticleSystem system in systems)
    {
        system.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    }

    Destroy(rootPs.gameObject, 1.5f);
 }

}
