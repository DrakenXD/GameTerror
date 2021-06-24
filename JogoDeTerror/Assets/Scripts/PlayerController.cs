using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private PlayerStats stats;
    private InputPlayer inputs;
    private Animator anim;
    private Rigidbody rb;
    private Vector3 velocity;

    [Header("          FlashLight")]

    [SerializeField] private Light FlashLight;
    [SerializeField] private Image barlight;
    [SerializeField] private bool On;
    [SerializeField] private bool Usable;
    private bool reload;
    [SerializeField] private float Time_OnFlashLight;
    [SerializeField] private float T_OnFlashLight;

    [Header("          SFX")]
    [SerializeField] private AudioSource SFX_TurnOnLight;
    [SerializeField] private AudioSource[] SFX_Walking;

    private void Awake()
    {
        stats = GetComponent<PlayerStats>();

        inputs = GetComponent<InputPlayer>();

        anim = GetComponent<Animator>();

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
        OnFlashLight();
    }

    private void Movement()
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

            ChangeAnimationState("Walking");
        }
        else
        {
            ChangeAnimationState("Idle");
            rb.velocity = Vector3.zero;
        }
    }

    #region FlashLight
    private void OnFlashLight()
    {
        if (On)
        {
            //verifica se o tempo é maior que 0
            if (T_OnFlashLight > 0)
            {

                //verificar se a carga é menor ou igual 30% da bateria
                if (T_OnFlashLight <= Time_OnFlashLight/3)
                {
                    Usable = false;
                    //animação de luz piscando
                    StartCoroutine("animFlashLight");
                }

                //usando a bateria
                T_OnFlashLight -= Time.deltaTime;

                UpdateBar(barlight, T_OnFlashLight, Time_OnFlashLight);
            }
            else if (T_OnFlashLight <= 0)
            {
                //desliga tudo quando estiver sem bateria
                On = false;
                FlashLight.enabled=(false);
            }

        }
        else
        {
          
            //verificar se tempo da bateria está descarregada
            if (reload)
            {
                if (T_OnFlashLight < Time_OnFlashLight)
                {
                    //carregando a bateria
                    T_OnFlashLight += Time.deltaTime;

                    UpdateBar(barlight, T_OnFlashLight, Time_OnFlashLight);

                    FlashLight.enabled = (false);

                    //verificar se a carga é maior ou igual 30% da bateria
                    if (T_OnFlashLight >= Time_OnFlashLight / 3) Usable = true;

                }
                else if (T_OnFlashLight >= Time_OnFlashLight)
                {
                    reload = false;
                    T_OnFlashLight = Time_OnFlashLight;
                }
                   
            }
            else
            {
                FlashLight.enabled = (false);
                StartCoroutine("TimeToReloadFlashLight");
            }
               

        }

        

        if (Input.GetKeyDown(inputs.FlashLight) && !On && Usable)
        {
            reload = false;
            SFX_TurnOnLight.Play();
            On = true;
            FlashLight.enabled = (true);
        }
        else if (Input.GetKeyDown(inputs.FlashLight) && On) 
        {
            reload = false;
            SFX_TurnOnLight.Play();
            On = false;
            FlashLight.enabled = (false);
        }
    }
    IEnumerator animFlashLight()
    {
        FlashLight.enabled = (false);
        yield return new WaitForSeconds(.2f);
        FlashLight.enabled = (true);
    }
    IEnumerator TimeToReloadFlashLight()
    {
        yield return new WaitForSeconds(1f);
        reload = true;
    }
    #endregion;

    private void UpdateBar(Image bar,float min,float max)
    {
        bar.fillAmount = min / max;
    }

    private string currentState;
    private void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        anim.Play(newState);

        currentState = newState;
    }

    public void SFXWalking()
    {
        int rd = Random.Range(0, SFX_Walking.Length);

        SFX_Walking[rd].Play();
    }
}
