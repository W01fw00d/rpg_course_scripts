using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonCharacterController : MonoBehaviour
{
    public float speed = 5.0f;
    public float horizontal = 0.0f;
    public float vertical = 0.0f;
    public bool attack = false;
    public bool jump = false;
    public bool die = false; //muriendo, transición animación
    public bool superattack = false;

    public bool hit = false;

    public bool dead = false; //muerto, estado
    public Vector3 moveDirection = Vector3.zero;

    private Animator animator;
    private Rigidbody rigidbody;

    private Transform dragonMouth;

    float maxSpeed = 5.0f;

    float rotationSpeed = 90;

    public GameObject fireballPrefab;
    private GameObject currentFireball;

    public float fireSpeed = 50;
    private bool fireball = false;

    private bool fireballLoaded = true;

    private const string
        ANIM_SPEED = "speed",
        ANIM_HORIZONTAL = "horizontal",
        ANIM_VERTICAL = "vertical",
        ANIM_ATTACK = "attack",
        ANIM_HIT = "hit",
        ANIM_DIE = "die",
        ANIM_IDLE = "idle",
        ANIM_RUN = "run",

        ANIM_JUMP = "jump";

    void Awake()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();

        dragonMouth = GameObject.Find("DragonMouth").transform;
        currentFireball = GameObject.Find("FireBall");
        //Instantiate(fireballPrefab, dragonMouth);
    }

    void Update()
    {
        if (dead)
        {
            if (die)
            {
                animator.SetBool(ANIM_DIE, true);
                die = false;

                //Invoke("Destroy", 5.0f);
            }

            return;
        }


        if (!attack && Input.GetKeyDown(KeyCode.C))
        {
            attack = true;
            animator.SetBool(ANIM_ATTACK, attack);
        }

        if (attack && Input.GetKeyUp(KeyCode.C))
        {
            attack = false;
            animator.SetBool(ANIM_ATTACK, attack);
        }

        if (!jump && Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
            animator.SetBool(ANIM_JUMP, jump);
        }

        if (jump && Input.GetKeyUp(KeyCode.Space))
        {
            jump = false;
            animator.SetBool(ANIM_JUMP, jump);
        }

        if (!hit && Input.GetKeyDown(KeyCode.H))
        {
            hit = true;
            animator.SetBool(ANIM_HIT, hit);
        }

        if (hit && Input.GetKeyUp(KeyCode.H))
        {
            hit = false;
            animator.SetBool(ANIM_HIT, hit);
        }

        // Debug death
        if (Input.GetKeyUp(KeyCode.I))
        {
            die = true;
            dead = true;
        }

        if (!fireball && fireballLoaded && Input.GetKeyDown(KeyCode.Mouse1))
        {
            fireballLoaded = false;

            currentFireball.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            currentFireball.GetComponent<Rigidbody>().AddForce(
                transform.forward * fireSpeed,
                ForceMode.Impulse
            );
            currentFireball.transform.parent = null;
            currentFireball.GetComponent<AudioSource>().volume = GameMaster.sharedInstance.sfxVolume;
            currentFireball.GetComponent<AudioSource>().Play();

            currentFireball.GetComponent<SphereCollider>().enabled = true;

            Invoke("LoadNewFireBall", 2.0f);

            fireball = true;
        }

        if (fireball && Input.GetKeyUp(KeyCode.Mouse1))
        {
            fireball = false;
        }
    }

    private void LoadNewFireBall()
    {
        currentFireball.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        currentFireball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        currentFireball.transform.position = dragonMouth.position;
        currentFireball.transform.parent = dragonMouth;
        currentFireball.GetComponent<SphereCollider>().enabled = false;

        fireballLoaded = true;
    }

    private void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        speed = new Vector2(horizontal, vertical).sqrMagnitude;

        rigidbody.velocity = 
            transform.forward * vertical * maxSpeed +
            new Vector3(0, rigidbody.velocity.y, 0);

        // Espacio = Velocidad * Tiempo ; cuanto más pulsada la tecla, más "Tiempo" recorres
        transform.Rotate(0, horizontal * rotationSpeed * Time.deltaTime, 0);

        animator.SetFloat(ANIM_SPEED, speed);
        animator.SetFloat(ANIM_HORIZONTAL, horizontal);
        animator.SetFloat(ANIM_VERTICAL, vertical);
    }
}
