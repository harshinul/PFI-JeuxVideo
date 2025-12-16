using UnityEngine;

public class PlayerAnimationComponent : MonoBehaviour
{
    Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        anim = GetComponent<Animator>();   
    }

    //************* Death *************//
    public void PlayDeathAnimation()
    {
        anim.SetTrigger("isDead");
    }

    //************* Attack *************//

    public void StartAttack(string parametersName)
    {
        anim.SetBool(parametersName, true);
    }

    public void StopAttack(string parametersName)
    {
        anim.SetBool(parametersName, false);
    }


    //************* Running *************//

    public void StartRunning()
    {
        anim.SetBool("isRunning", true);
    }

    public void StopRunning()
    {
        anim.SetBool("isRunning", false);
    }

    //************* Walking *************//

    public void StartWalking()
    {
        anim.SetBool("isWalking", true);
    }

    public void StopWalking()
    {
        anim.SetBool("isWalking", false);
    }

    //************* Equip *************//

    public void EquipNone()
    {
        EquipBaseballBat(); // Same animation as baseball bat
    }
    public void EquipBaseballBat()
    {
        UnequipWeapons();
        anim.SetBool("IsEquipBaseballBat", true);
    }

    public void EquipPistol()
    {
        UnequipWeapons();
        anim.SetBool("IsEquipPistol", true);
    }

    public void EquipRiffle()
    {
        UnequipWeapons();
        anim.SetBool("IsEquipRiffle", true);
    }

    public void UnequipWeapons()
    {
        anim.SetBool("IsEquipBaseballBat", false);
        anim.SetBool("IsEquipPistol", false);
        anim.SetBool("IsEquipRiffle", false);
    }



}
