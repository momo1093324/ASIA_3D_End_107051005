﻿using UnityEngine;
using Invector.vCharacterController;  //引用套件

public class Player : MonoBehaviour
{
    private float hp = 100;
    private Animator ani;

    private void Awake()
    {
        ani = GetComponent<Animator>();

    }
    /// <summary>
    /// 受傷
    /// </summary>
    public void Damage()
    {
        hp -= 35;
        ani.SetTrigger("受傷觸發");

        if (hp <= 0) Dead();
    }
    /// <summary>
    /// 死亡觸發
    /// </summary>
    private void Dead()
    {
        ani.SetTrigger("死亡開關");

        //鎖定移動與旋轉
        vThirdPersonController vt = GetComponent<vThirdPersonController>();
        vt.lockMovement = true;
        vt.lockRotation = true;
    }
}