using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerStats stats;
    private InputPlayer inputs;

    private Rigidbody rb;
    private Vector3 velocity;

    [Header("          FlashLight")]
    public bool On;
    public float Time_OnFlashLight;
    private float T_OnFlashLight;

    private void Awake()
    {
        stats = GetComponent<PlayerStats>();

        inputs = GetComponent<InputPlayer>();

        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        T_OnFlashLight = Time_OnFlashLight;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    public void Movement()
    {
        if (Input.GetKey(inputs.Foward))
        {
            velocity.z = 1;

        }
        else if (Input.GetKeyUp(inputs.Foward))
        {

            velocity.z = 0;
        }

        if (Input.GetKey(inputs.Backward))
        {
            velocity.z = -1;

        }
        else if (Input.GetKeyUp(inputs.Backward))
        {

            velocity.z = 0;
        }

        if (Input.GetKey(inputs.Right))
        {
            velocity.x = 1;
      
        }
        else if (Input.GetKeyUp(inputs.Right))
        {
   
            velocity.x = 0;
        }

        if (Input.GetKey(inputs.Left))
        {
            velocity.x = -1;
          
        }
        else if (Input.GetKeyUp(inputs.Left))
        {
         
            velocity.x = 0;
        }


        if (velocity.x != 0 | velocity.z != 0)
        {
            Vector3 soma = transform.right * velocity.x + transform.forward * velocity.z;

            rb.velocity = soma * stats.Speed ;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

   
}
