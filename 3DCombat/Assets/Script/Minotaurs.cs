using UnityEngine;

public class Minotaurs : MonoBehaviour
{
    public Animator minoAnim;
    public Transform target;
    public float minoSpeed;
    bool enableAct;
    int atkStep;

    void Start()
    {
        minoAnim = GetComponent<Animator>();
        enableAct = true;
    }

    void RotateMino()
    {
        Vector3 dir = target.position - transform.position;
        //Slerp 회전 부드럽게
        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.LookRotation(dir), Time.deltaTime * 5f);
    }

    void MoveMino()
    {
        if ((target.position - transform.position).magnitude >= 3)
        {
            minoAnim.SetBool("Walk", true);
            transform.Translate(Vector3.forward * minoSpeed * Time.deltaTime, Space.Self);

        }
        if ((target.position - transform.position).magnitude < 3)
        {
            minoAnim.SetBool("Walk", false);
        }
    }



    void Update()
    {
        if (enableAct)
        {
            RotateMino();
            MoveMino();
        }
    }

    void MinoAtk()
    {
        if ((target.position - transform.position).magnitude < 3)
        {
            switch (atkStep)
            {
                case 0:
                    atkStep++;
                    minoAnim.Play("attack1");
                    break;
                case 1:
                    atkStep++;
                    minoAnim.Play("attack2");
                    break;
                case 2:
                    atkStep++;
                    minoAnim.Play("attack3");
                    break;
            }
        }
    }

    void FreezeMino()
    {
        enableAct = false;
    }

    void UnFreezeMino()
    {
        enableAct = true;
    }
}
