using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC_Barbarian : MonoBehaviour {

    private Animator animator;
    private NavMeshAgent nav;

    private SphereCollider collider;

    public GameObject player;

    public float speed = 0.0f;
    public float h = 0.0f;
    public float v = 0.0f;

    public bool attack = false;
    public bool jump = false;
    public bool die = false;

    public bool DEBUG = false;
    public bool DEBUG_DRAW = false;

    public Vector3 direction; //Donde está player en relación a NPC
    public float distance = 0.0f; //Entre player y NPC
    public float angle = 0.0f; //Ángulo entre jugador y NPC
    public bool playerInSight = false;
    public float fieldOfViewAngle = 120;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        collider = GetComponent<SphereCollider>();

        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        if (die)
        {
            //ToDo animar la muerte del NPC
        }

		if (playerInSight)
        {
            this.transform.rotation = Quaternion.Slerp(
                this.transform.rotation,
                Quaternion.LookRotation(direction),
                0.2f
            );
        }

        if (IsPlayerBarbarianCharacterDead())
        {
            animator.SetBool("attack", false);
            animator.SetFloat("linearSpeed", 0);
            animator.SetFloat("angularSpeed", 0);
            // ???
        }
	}

    private bool IsPlayerBarbarianCharacterDead()
    {
        //ToDo use Player tag check to known the correct Controller to use depending on selected player ...
        return player.transform.GetComponent<BarbarianCharacterController>().dead;
    }

    private void FixedUpdate()
    {
        h = angle;
        v = distance;
        speed = distance / Time.deltaTime;

        if (DEBUG)
        {
            Debug.Log(string.Format("H:{0} - V:{1}, S:{2}", h, v, speed));
        }

        animator.SetFloat("linearSpeed", speed);
        animator.SetFloat("angularSpeed", h);
        animator.SetBool("attack", attack);

        if (attack)
        {
            animator.SetBool("attack", true);
        }

        if (playerInSight)
        {
            if (animator.GetFloat("attack2") > 0.5f || animator.GetFloat("attack3") > 0.5f)
            {
                float baseDamage = 10.0f;
                float enemyAttack = GetComponent<NPCAgent>().npcData.strength;
                float playerDefense = player.GetComponent<PlayerAgent>().playerCharacterData.defense;

                //TODO: Influence damage by strenght of others
                //TODO: si hay armas equipadas o armaduras lo suyo es hacer un bucle e incrementar el ataque/defensa según los modificadores de arma y armadura
                player.GetComponent<PlayerAgent>().playerCharacterData.health -=
                    baseDamage * Time.deltaTime * enemyAttack / playerDefense;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag.Equals("Player") && !IsPlayerBarbarianCharacterDead())
        {
            //vector = destino - origen
            direction = other.transform.position - this.transform.transform.position;
            distance = Vector3.Magnitude(direction) - 1.0f;
            angle = Vector3.Dot(this.transform.forward, player.transform.position);

            if (DEBUG_DRAW)
            {
                Debug.DrawLine(
                    this.transform.position + Vector3.up,
                    direction * 50,
                    Color.magenta
                );

                Debug.DrawLine(
                   player.transform.position,
                   this.transform.position,
                   Color.blue
               );
            }

            playerInSight = false;

            float calculateAngle = Vector3.Angle(direction, transform.forward);

            if (calculateAngle < 0.5f * fieldOfViewAngle)
            {
                RaycastHit hit;

                if (DEBUG_DRAW)
                {
                    Debug.DrawRay(
                        this.transform.position + transform.up,
                        direction.normalized,
                        Color.green
                    );
                }

                // Trazo un rayo entre NPC y player
                if (Physics.Raycast(
                    transform.position + transform.up,
                    direction.normalized,
                    out hit,
                    collider.radius
                )){

                    // Si lo primero que veo es el player
                    if (hit.collider.gameObject == player)
                    {
                        playerInSight = true;

                        if (DEBUG)
                        {
                            Debug.Log("Jugador en el campo de visión");
                        }
                    }
                }
            }

            //Si depsués de toda la comprobación anterior, el player está en FoV del NPC
            if (playerInSight)
            {
                nav.SetDestination(player.transform.position);
                CalculatePathLength(player.transform.position);
                // Si estoy muy cerca, puedo atacar...

                attack = distance < 1.1f;
            }
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag.Equals("Player"))
        {
            distance = 0;
            angle = 0;
            attack = false;
        }
    }

    float CalculatePathLength(Vector3 targetPosition)
    {
        NavMeshPath path = new NavMeshPath();

        if (nav.enabled)
        {
            nav.CalculatePath(targetPosition, path);
            Vector3[] allTheWayPoints = new Vector3[path.corners.Length + 2];
            allTheWayPoints[0] = this.transform.position;
            allTheWayPoints[allTheWayPoints.Length - 1] = targetPosition;

            for (int i = 1; i < path.corners.Length - 1; i++)
            {
                allTheWayPoints[i + 1] = path.corners[i];
            }

            float pathLength = 0;

            for (int i = 0; i > allTheWayPoints.Length - 1; i++)
            {
                pathLength += Vector3.Distance(allTheWayPoints[i], allTheWayPoints[i + 1]);

                if (DEBUG_DRAW)
                {
                    Debug.DrawLine(
                        allTheWayPoints[i],
                        allTheWayPoints[i + 1],
                        Color.yellow
                    );
                }
            }

            return pathLength;
        }

        return 0;
    }
}
