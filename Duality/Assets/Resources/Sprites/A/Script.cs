using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script : MonoBehaviour
{
    public static bool esterEgg = false;

    /// To jest super sekretny skrypt, jeżeli go zobaczyłeś...
    /// 
    /// To w sumie nwm, ale nic nie widziałeś pytanie NIE zadawaj
    /// 
    /// W każdym razie udawaj, że ten skrypt nie istnieje

    bool fired = false;

    private void Start()
    {
        if (esterEgg && !fired)
        {
            fired = true;
            SuperSecretEsterEgg();
        }
        
    }

    private void Update()
    {
        if (!esterEgg && Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.C) && Input.GetKey(KeyCode.O))
        {
            Debug.Log("Ester Egg activated!");
            esterEgg = true;
        }

        if (!esterEgg && Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.Delete))
        {
            Debug.Log("Ester Egg activated!");
            esterEgg = true;
        }
    }

    private void SuperSecretEsterEgg()
    {
        PlayerController.Instance.GetComponentInChildren<CharacterAnimator>().framesLocation = "A";
        PlayerController.Instance.GetComponentInChildren<CharacterAnimator>().ReloadAnimations();
        PlayerController.Instance.GetComponentInChildren<CharacterAnimator>().Play("idle");
    }
}
