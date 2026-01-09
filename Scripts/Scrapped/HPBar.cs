using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public Image fillImage;
    StatBlock target;

    public void Bind(StatBlock stat){
        target = stat;
        UpdateBar();
    }

    void Update(){
        if (target == null){
            return;
        }
        UpdateBar();
    }

    public void UpdateBar()
    {
        if (target == null){
            return;
        }

        fillImage.fillAmount = (float)target.currentHP/target.maxHP;
    }
}
