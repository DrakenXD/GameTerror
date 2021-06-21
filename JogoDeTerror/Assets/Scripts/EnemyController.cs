using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform[] pontoDeCaminho;
    [SerializeField] private int AIpontoAtual;

    [SerializeField] Transform[] LinhadeVisao;
    [SerializeField] private bool VendoAlvo;
    [SerializeField] private float DistanciaDoAlvo,DistanciaDoPonto;
    [SerializeField] private float DistanciaDePercepcao = 30, DistanciaParaSeguir = 20, DistanciaParaAtacar = 2;
    [SerializeField] private bool PerseguindoAlvo,ContadorDePerguicao,AtacandoAlvo;
    [SerializeField] private float MaxTempoDePerseguicao, CronometroDePerseguicao;
    [SerializeField] private float MaxTempoParado, CronometroDeTempoParado;

    private NavMeshAgent AI;
    private Transform alvo;
    private EnemyStats stats;
    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<EnemyStats>();

        AI = GetComponent<NavMeshAgent>();

        AIpontoAtual = Random.Range(0,pontoDeCaminho.Length);
    }

    // Update is called once per frame
    void Update()
    {
        SearchTarget();

        DistanciaDoAlvo = Vector3.Distance(alvo.position, transform.position);

        DistanciaDoPonto = Vector3.Distance(pontoDeCaminho[AIpontoAtual].position, transform.position);

        for (int i = 0; i < LinhadeVisao.Length; i++) 
        {
            if (Physics.Raycast(LinhadeVisao[i].position, LinhadeVisao[i].forward, out RaycastHit hit, 1000) && DistanciaDoAlvo < DistanciaDePercepcao)
            {
                Debug.DrawLine(LinhadeVisao[i].position, hit.point, Color.red);

                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    VendoAlvo = true;

                }

            }
        }

  
    
        if (DistanciaDoAlvo <= DistanciaParaAtacar)
        {
            CarregandoAtaque();

        }else if (VendoAlvo)
        {
            if (DistanciaDoAlvo <= DistanciaParaSeguir)
            {
                SerguindoAlvo();
                PerseguindoAlvo = true;
                ContadorDePerguicao = true;
            }

        }
        else
        {
            Passear();
        }
       

        //COMANDO PARA PASSEAR
        if (DistanciaDoPonto <= 2)
        {
            if (CronometroDeTempoParado >= MaxTempoParado)
            {
                AIpontoAtual = Random.Range(0, pontoDeCaminho.Length);
                CronometroDeTempoParado = 0;
            }
            else CronometroDeTempoParado += Time.deltaTime;

            Passear();
        }

        //CONTADORES DE PERSEGUIÇÃO
        if (DistanciaDoAlvo >= DistanciaParaSeguir)
        {
            if (ContadorDePerguicao && VendoAlvo)
            {
                CronometroDePerseguicao += Time.deltaTime;
            }
            if (CronometroDePerseguicao >= MaxTempoDePerseguicao && VendoAlvo)
            {
                ContadorDePerguicao = false;
                CronometroDePerseguicao = 0;
                PerseguindoAlvo = false;
                VendoAlvo = false;
            }


        }

    }
    protected void Passear()
    {
        if (!PerseguindoAlvo)
        {
            AI.speed = stats.SpeedWalking;
            AI.SetDestination(pontoDeCaminho[AIpontoAtual].position);
        }

    }
    void Olhar()
    {
        AI.speed = 0;
        transform.LookAt(alvo);
    }
    protected void SerguindoAlvo()
    {
        AI.speed = stats.SpeedFollow;
        AI.SetDestination(alvo.position);
    }
    public void CarregandoAtaque()
    {
        //COLOCAR ANIMAÇÃO DE ATAQUE
    }
    public void Atacar()
    {

    }
    protected void SearchTarget()
    {
        GameObject[] Targets = GameObject.FindGameObjectsWithTag("Player");

        float shortestDistance = Mathf.Infinity;

        GameObject nearestTarget = null;

        foreach (GameObject Target in Targets)
        {
            float distancetoTarget = Vector2.Distance(transform.position, Target.transform.position);

            if (distancetoTarget < shortestDistance)
            {
                shortestDistance = distancetoTarget;
                DistanciaDoAlvo = shortestDistance;
                nearestTarget = Target;
            }
        }

        if (nearestTarget != null && shortestDistance <= 1000)
        {
            alvo = nearestTarget.transform;
        }

        if (alvo == null)
        {
            return;
        }

    }
    
}
