using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float angleBetween = 0.5f;

    public Vector3 BulletDirection;
    public Vector3 LastMousePosition;

    public float BulletLife;
    float speed;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        BulletLife = 50;
        speed = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        BulletLife = BulletLife - 0.5f;

        Vector3 Aim = BulletDirection;
        
            
        //transform.Translate(Vector3.forward * speed * Time.deltaTime);

        transform.position += transform.forward * speed * Time.deltaTime;

        //Vector3 newPosition = Vector3.MoveTowards(transform.position, Aim, speed * Time.deltaTime);

        // Update the object's position

        //transform.position = newPosition;

        if (BulletLife < 1)
        {
            Destroy(gameObject);
        }

        //Bullet will communicate with cursor script, cursor script will talk to object/beast script that detects when and when not the cursor
        //is ontop of it. Expect function like <getcomponent> "HoveredObject"
    }
}
