using TMPro;
using UnityEngine;

public class Col_PlayerAtk : MonoBehaviour
{
    public Combo combo;
    public string type_Atk;

    int comboStep;
    public string dmg;
    public TextMeshProUGUI dmgText;

    public HP_Mino hp_Mino;
    public TextMeshProUGUI dmgValue;
    public HitStop hitStop;

    public GameObject hitEffect;

    private void OnEnable()
    {
        comboStep = combo.comboStep;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HitBox_Enemy"))
        {
            Instantiate(hitEffect, transform.position, Quaternion.identity);

            dmg = string.Format("{0}+{1}", type_Atk, comboStep);
            dmgText.text = dmg;

            dmgValue.text = $"{hp_Mino.dmg}";

            dmgText.gameObject.SetActive(true);
            dmgValue.gameObject.SetActive(true);
            hitStop.StopTime();
        }
    }



}
