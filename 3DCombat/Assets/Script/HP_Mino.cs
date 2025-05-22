using UnityEngine;
using UnityEngine.UI;

public class HP_Mino : MonoBehaviour
{
    public float hp;
    public float hp_Cur;
    public float dmg;

    public Image hpBar_Front;
    public Image hpBar_Back;
    void Start()
    {
        hp_Cur = hp;
    }

    void SyncBar()
    {
        hpBar_Front.fillAmount = hp_Cur / hp;

        if (hpBar_Back.fillAmount > hpBar_Front.fillAmount)
        {
            hpBar_Back.fillAmount = Mathf.Lerp(hpBar_Back.fillAmount, hpBar_Front.fillAmount, Time.deltaTime);
        }
    }

    void Update()
    {
        SyncBar();
    }

    public GameObject Mino;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Col_PlayerAttack"))
        {
            dmg = Random.Range(100, 500);

            hp_Cur -= dmg;
            if (hp_Cur <= 0)
            {
                Mino.GetComponent<Minotaurs>().minoAnim.Play("knockDown");
                GameObject go = GameObject.Find("HPBar_Boss");
                if (go != null)
                    go.SetActive(false);
                Destroy(Mino, 3);
            }
        }
    }


}
