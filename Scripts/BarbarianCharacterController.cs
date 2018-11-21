using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarbarianCharacterController : MonoBehaviour
{
    public float speed = 5.0f;
    public float horizontal = 0.0f;
    public float vertical = 0.0f;
    public bool attack = false;
    public bool jump = false;
    public bool die = false; //muriendo, transición
    public bool superattack = false;

    public bool run = false;
    public bool dead = false; //muerto, estado
    public Vector3 moveDirection = Vector3.zero;

    private Animator animator;

    public BaseCharacter characterData;

    private const string
        ANIM_SPEED = "speed",
        ANIM_HORIZONTAL = "horizontal",
        ANIM_VERTICAL = "vertical",
        ANIM_JUMP = "jump",
        ANIM_ATTACK = "attack",
        ANIM_SUPER_ATTACK = "super-attack",
        ANIM_DIE = "die",
        ANIM_IDLE = "idle",
        ANIM_RUN = "run";

    void Start () {
        animator = GetComponent<Animator>();

        // Investigar este bug, parece que al principio no se cumplen las condiciones para renderizar
        Invoke("RenderWeapon", 0.1f); 
	}
	
	void Update () {
		if (dead)
        {
            if (die)
            {
                animator.SetBool(ANIM_DIE, true);
                die = false;
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

        if (!superattack && Input.GetKeyDown(KeyCode.V))
        {
            superattack = true;
            animator.SetBool(ANIM_SUPER_ATTACK, superattack);
        }

        if (superattack && Input.GetKeyUp(KeyCode.V))
        {
            superattack = false;
            animator.SetBool(ANIM_SUPER_ATTACK, superattack);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            run = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            run = false;
        }

        animator.SetBool(ANIM_RUN, run);


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

        // Debug death
        if (Input.GetKeyUp(KeyCode.I))
        {
            die = true;
            dead = true;
        }
    }

    private void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        speed = new Vector2(horizontal, vertical).sqrMagnitude;

        animator.SetFloat(ANIM_SPEED, speed);
        animator.SetFloat(ANIM_HORIZONTAL, horizontal);
        animator.SetFloat(ANIM_VERTICAL, vertical);
    }


    public void RenderWeapon()
    {
        if (!characterData.canUseWeapons)
        {
            return;
        }

        if (characterData.currentWeapon != null && characterData.weaponSpot != null)
        {
            characterData.currentWeapon.transform.position = characterData.currentWeapon.GetComponent<InventoryItemAgent>().playerPosition;
            //characterData.currentWeapon.transform.localRotation = characterData.currentWeapon.GetComponent<InventoryItemAgent>().playerRotation;

            // disable InventoryItemAgent when I create the script

            //currentWeapon.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;


            //if (lastWeapon != null)
            //{
            //    Destroy(lastWeapon);
            //}
            Instantiate(characterData.currentWeapon, characterData.weaponSpot);
        }
    }
}
