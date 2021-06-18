using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float VelocidadePasseio;
    [SerializeField] private float VelocidadePerseguindoAlvo;
    private NavMeshAgent AI;

    [SerializeField] private Transform alvo;
    [SerializeField] private Transform[] pontoDeCaminho;
    [SerializeField] private int AIpontoAtual;

    [SerializeField] private bool VendoAlvo;
    [SerializeField] private float DistanciaDoAlvo,DistanciaDoPonto;
    [SerializeField] private float DistanciaDePercepcao = 30, DistanciaParaSeguir = 20, DistanciaParaAtacar = 2;
    [SerializeField] private bool PerseguindoAlvo,ContadorDePerguicao;
    [SerializeField] private float CronometroDePerseguicao;

    // Start is called before the first frame update
    void Start()
    {
        AI = GetComponent<NavMeshAgent>();
        AIpontoAtual = Random.Range(0,pontoDeCaminho.Length);
    }

    // Update is called once per frame
    void Update()
    {
        DistanciaDoAlvo = Vector3.Distance(alvo.position,transform.position);
        DistanciaDoPonto = Vector3.Distance(pontoDeCaminho[AIpontoAtual].position,transform.position);

        RaycastHit hit;

        Vector3 direcao = transform.position - alvo.position;

        if (Physics.Raycast(transform.position,direcao,out hit,1000) && DistanciaDoAlvo < DistanciaDePercepcao)
        {
            if (hit.collider.CompareTag("Player"))
            {
                VendoAlvo = true;
            }
            else
            {
                VendoAlvo = false;
            }
        }
    }
}
