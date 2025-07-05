using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Rotation : MonoBehaviour
{
    Vector3 rot;
    float Speed;
    float inc;
    float armspeed = 0.1f;
    float Forwardvar;
    public Vector3 rotation;
    public AnimationCurve curve;
    public float time;
    float curvevalue;

    bool rotatingrn;

    // Start is called before the first frame update
    void Start()
    {
        Speed = 2f;
        inc = 25;
        
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        //Quaternion rotation = transform.localRotation;


        float curvevalue = curve.Evaluate(time);
        
        
        if (Input.GetKey(KeyCode.W))
        {
            //loc
            pos.x += Speed * Time.deltaTime;

            rotatingrn = true;
            if (time < 1){
                time += 0.03f;
            }

            curvevalue = curve.Evaluate(time);
            Forwardvar = 0 * curvevalue;
            rotation = new Vector3(0,Forwardvar,0);


            //rotation = new Vector3(0, 0, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            //loc
            pos.z -= Speed * Time.deltaTime;

            rotatingrn = true;
            if (time < 1)
            {
                time += 0.03f;
            }

            curvevalue = curve.Evaluate(time);
            float Rightvar = 90 * curvevalue;
            rotation = new Vector3(0, Rightvar, 0);

            //rotation = new Vector3(0, 90, 0);

            
        }

        if (Input.GetKey(KeyCode.A))
        {
            //loc
            pos.z += Speed * Time.deltaTime;

            rotatingrn = true;
            if (time < 1)
            {
                time += 0.03f;
            }

            curvevalue = curve.Evaluate(time);
            float Leftvar = -90 * curvevalue;
            rotation = new Vector3(0, Leftvar, 0);

            //rotation = new Vector3(0, 270, 0);

          
        }

        if (Input.GetKey(KeyCode.S))
        {
            //loc
            pos.x -= Speed * Time.deltaTime;

            rotatingrn = true;
            if (time < 1)
            {
                time += 0.03f;
            }

            curvevalue = curve.Evaluate(time);
            float Backvar = 180 * curvevalue;
            //float properback = Mathf.Atan2(,) * Mathf.Rad2Deg;
            rotation = new Vector3(0, Backvar, 0);

            //rotation = new Vector3(0, 180, 0);

        }



        rotatingrn = false;

        if (!rotatingrn)
        {
            if (time > 0) {
                time -= 0.01f;
            }
            
         }

        //transform.position = pos;

        transform.localRotation = Quaternion.Euler(rotation);
    }
}
