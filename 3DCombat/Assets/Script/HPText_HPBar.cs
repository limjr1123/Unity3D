using TMPro;
using UnityEngine;

public class HPText_HPBar : MonoBehaviour
{
    public TextMeshProUGUI hpText;
    [SerializeField] public HP_Mino MinoHp;

    void Start()
    {
        hpText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        hpText.text = $"{MinoHp.hp_Cur} / {MinoHp.hp}";
    }
}
