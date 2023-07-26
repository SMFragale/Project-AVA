using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereRobotAnimControl : MonoBehaviour
{
	Vector3 rot = Vector3.zero;
	Animator anim;
    public bool Roll_Anim { get => anim.GetBool("Roll_Anim"); set { anim.SetBool("Roll_Anim", value); } }
    public bool Walk_Anim { get => anim.GetBool("Walk_Anim"); set { anim.SetBool("Walk_Anim", value); } }
    public bool Open_Anim { get => anim.GetBool("Open_Anim"); set { anim.SetBool("Open_Anim", value); } }
    
	void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        gameObject.transform.eulerAngles = rot;
        Roll_Anim = false;
        Walk_Anim = false;
        Open_Anim = true;
    }

    void Start()
    {
        anim.SetBool("Walk_Anim", Walk_Anim);
        anim.SetBool("Open_Anim", Open_Anim);
        anim.SetBool("Roll_Anim", Roll_Anim);
    }

    public AnimatorStateInfo GetCurrentStateInfo()
    {
        return anim.GetCurrentAnimatorStateInfo(0);
    }



}
