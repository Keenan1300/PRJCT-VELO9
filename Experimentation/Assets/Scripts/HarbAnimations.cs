using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using System.Collections;
using UnityEngine.Events;

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


    public Animator anim;

    //anim controllers
    public RuntimeAnimatorController HarbingerEquipped;
    public RuntimeAnimatorController HarbingerUnequipped;

    //bool
    private bool equipped;


    void Start()
    {


        equipped = true;
        anim = GetComponent<Animator>();

        // Set the default controller initially
        anim.runtimeAnimatorController = HarbingerEquipped;

        anim.Update(7f);
        anim.speed = 1f;

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
            anim.SetBool("Idle", true);

            if ((Input.GetKey(KeyCode.W)))
            {
                // Keep character pinned to the ground without sliding
                //velocity.y = groundedGravity;
                anim.SetBool("Idle", false);
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
                anim.runtimeAnimatorController = HarbingerEquipped;
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

}