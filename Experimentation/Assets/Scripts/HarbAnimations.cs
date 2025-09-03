using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using System.Collections;
using UnityEngine.Events;
using Unity.VisualScripting;

public class HarbAnimations : MonoBehaviour
{
    //separate rotation from movement
    private Transform aimTarget;


    //retrieve GameObject info
    public GameObject PlayerBody;
    public Here PlayerData;

    //variables
    private float moveSpeed = 17;
    private float gravity = 0;
    private float groundedGravity = 0;
    private CharacterController controller;
    private Vector3 velocity;

    public float x;
    public float y;


    public Animator anim;

    //anim controllers
    public RuntimeAnimatorController HarbingerEquipped;

    //test
    public RuntimeAnimatorController HarbingerEquippedv2;

    public RuntimeAnimatorController HarbingerUnequipped;

    //bool
    private bool equipped;


    void Start()
    {

        equipped = true;
        anim = GetComponent<Animator>();

        // Set the default controller initially
        anim.runtimeAnimatorController = HarbingerEquippedv2;

        anim.Update(7f);
        anim.speed = 1f;

        //Set Animation bools
        anim.SetBool("Idle", true);
        anim.SetBool("RightStrafe", false);
        anim.SetBool("LeftStrafe", false);
        anim.SetBool("BackStrafe", false);
        anim.SetBool("Run", false);
        anim.SetBool("Grab", false);
        anim.SetBool("GunFire", false);

    }

    void Update()
    {
        if (equipped)
        {
            //naturally return to idle
            y = Input.GetAxisRaw("Vertical");
            x = Input.GetAxisRaw("Horizontal");
            anim.SetFloat("x", x);
            anim.SetFloat("y", y);
            print("x value" + x);

            if ((Input.GetKey(KeyCode.W)))
            {
                // Keep character pinned to the ground without sliding
                //velocity.y = groundedGravity;
                anim.SetBool("Idle", false);
               
                anim.SetFloat("y", y);
                

                anim.SetBool("Run", true);
            }
            else
            {
                anim.SetBool("Run", false);
            }

            if ((Input.GetKey(KeyCode.D)))
            {
                // Keep character pinned to the ground without sliding
                //velocity.y = groundedGravity;
                anim.SetBool("Idle", false);
                anim.SetBool("RightStrafe", true);
                
                    anim.SetFloat("x", x);
                
            }
            else
            {
                anim.SetBool("RightStrafe", false);
                

            }

            if ((Input.GetKey(KeyCode.A)))
            {
                // Keep character pinned to the ground without sliding
                //velocity.y = groundedGravity;
                anim.SetBool("Idle", false);
                anim.SetBool("LeftStrafe", true);
                anim.SetFloat("x", x);
            }
            else
            {
                anim.SetBool("LeftStrafe", false);
                

            }

            if ((Input.GetKey(KeyCode.S)))
            {
                // Keep character pinned to the ground without sliding
                //velocity.y = groundedGravity;
                anim.SetBool("Idle", false);
                anim.SetBool("BackStep", true);
                anim.SetFloat("y", y);
            }
            else
            {
                anim.SetBool("BackStep", false);
                
            }


        }

        if (!equipped)
        {
            anim.SetBool("Idle", true);
            anim.SetBool("Run", false);

            if ((Input.GetKey(KeyCode.W)))
            {
                // Keep character pinned to the ground without sliding
                //velocity.y = groundedGravity;
                anim.SetBool("Idle", false);
                anim.SetBool("Run", true);
            }
            

            if ((Input.GetKey(KeyCode.D)))
            {
                // Keep character pinned to the ground without sliding
                //velocity.y = groundedGravity;
                anim.SetBool("Idle", false);
                anim.SetBool("Run", true);
            }


            if ((Input.GetKey(KeyCode.A)))
            {
                // Keep character pinned to the ground without sliding
                //velocity.y = groundedGravity;
                anim.SetBool("Idle", false);
                anim.SetBool("Run", true);
            }
            

            if ((Input.GetKey(KeyCode.S)))
            {
                // Keep character pinned to the ground without sliding
                //velocity.y = groundedGravity;
                anim.SetBool("Idle", false);
                anim.SetBool("Run", true);
            }
        
              

        }


        if ((Input.GetKeyDown(KeyCode.E)))
        {
            if (equipped)
            {
                anim.runtimeAnimatorController = HarbingerUnequipped;
                equipped = false;
            }

            else if (!equipped)
            {
                anim.runtimeAnimatorController = HarbingerEquippedv2;
                equipped = true;

            }
        }
    }




    public void SwitchToEquipped()
    {
        equipped = true;
    }




    public void SwitchtoWASD()
    {
        equipped = false;
    }



    public void FireGun()
    {
        print("pt2");
        anim.SetBool("Idle", false);
        anim.SetBool("GunFire", true);
       
    }
    public void NOFireGun()
    {
        anim.SetBool("GunFire", false);

    }


    public void Reset()
    {

        anim.SetBool("Idle", true);
        anim.SetBool("RightStrafe", false);
        anim.SetBool("LeftStrafe", false);
        anim.SetBool("BackStrafe", false);
        anim.SetBool("Run", false);
        anim.SetBool("Grab", false);
        anim.SetBool("GunFire", false);

    }

}