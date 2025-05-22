using UnityEngine;
using UnityEngine.UI;

public class HP_Player : MonoBehaviour
{
    public float hp;
    public float hp_Cur;

    public Image hpBar_Front;
    public Image hpBar_Back;

    void Start()
    {
        hp_Cur = hp;
    }

    void syncBar()
    {
        hpBar_Front.fillAmount = hp_Cur / hp;

        if(hpBar_Back.fillAmount > hpBar_Front.fillAmount)
        {
            hpBar_Back.fillAmount = Mathf.Lerp(hpBar_Back.fillAmount, hpBar_Front.fillAmount, Time.deltaTime);
        }
    }

    void Update()
    {
        syncBar();
    }

    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Col_EnemyAttack"))
        {
            hp_Cur -= Random.Range(100, 200);
            if (hp_Cur <= 0)
            {
                player.GetComponent<Combo>().playerAnim.Play("knockDown");
                GameObject go = GameObject.Find("HPBar_Player");
                if (go != null)
                    go.SetActive(false);
            }
        }
    }
}
