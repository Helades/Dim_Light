using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
//death
//cambio valor de vida
//disparar
//habilidad lanzar gancho
//dash
//salto
//energia
//usar objetos

public class PlayerController : MonoBehaviour
{
    // El controlador principal del jugador
    public enum StateIds
    {
        Idle,
        Walk,
        Jump,
        Fall,
        Dead
    }
    public StateIds estadoID = StateIds.Idle;
    public Rigidbody2D rb; //fisicas del personaje
    [Header("Posiciones de interes")]
    private Transform playerCamera; //referencia a la camara

    public Image hp1, hp2, hp3, hp4, hp5, hp6, hp7, hp8, hp9, hp10; //array
    public Transform energybar; // energía del personaje
    public Transform spotlight; // referencias luz de foco
    public Animator cynthia;
    public Light playerlight;
    public CanvasGroup cg;
    public GameObject player, ps;
    public AnimationCurve curve;
    public Vector2 impulseForce = new Vector2(400, 40);
    public bool isGrounded, isDead, isInmortal;
    private bool energystop, flashrdy, canDash, isDashing;
    public float speed, sprintspeed, fallMultiplier, lowJumpMultiplier, energy, smoothSpeed, acceleration;
    private float energycount, /*fallcountdown,*/ detectionDistance = 18f, counter, dashC;
    private Vector2 mousePos;
    public Vector3 offset;
    public int runes, key, lighthp, prehp;
    private int dash_layer;
    private float originalLocalScale;

    [Header("Events")]
    public UnityEvent OnDeath; //evento que se llama cuando la vida del jugador baja a 0 
    [Header("Ability Events")]
    public UnityEvent OnJump;
    public UnityEvent OnDash; //evento que se llama cuando el jugador dashea
    public UnityEvent OnFlash; //evento que se llama cuando el jugador usa el flash de la luz

    public PlayerController()
    {
        runes = 0;
        key = 0;
        speed = 600f;
        sprintspeed = 1350f;
        lighthp = 10;
        fallMultiplier = 5f;
        lowJumpMultiplier = 3.5f;
        energy = 100f;
        //fallcountdown = 0;
        detectionDistance = 18f;
        prehp = lighthp;
        dashC = 0;
        offset = new Vector3 (0, 0, -60);
        smoothSpeed = 0.01f;
        acceleration = 300f;

        isGrounded = true;
        energystop = false;
        flashrdy = true;
        isDead = false;
        isInmortal = false;
        canDash = true;
        isDashing = false;
    }

    #region Monobehaviour

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCamera = Camera.main.transform;

        cg.alpha = 0;
        Light();

        mousePos = Input.mousePosition;

        originalLocalScale = player.transform.localScale.x;
    }

    private void Update()
    {
        if (prehp > lighthp || prehp < lighthp)
        {
            lighthp = prehp;
            Light();
            if (lighthp > 0)
                isDead = false;
        }

        if (lighthp < 0)
        {
            isDead = true;
        }

        if (!isDead)
        {
            if (Input.GetButtonDown("Sprint") && energy > 0 && canDash && canDash && !isDashing)
            {
                OnDash.Invoke();
            }

            // invocación del salto
            if (isGrounded && Input.GetButtonDown("Jump") && energy > 0)
            {
                OnJump.Invoke();
            }

            // intensidad del salto
            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
            }
            else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
            }
            else if (rb.velocity.y > 0 && Input.GetButton("Jump") && energy > 0)
            {
                energy -= 15f * Time.fixedDeltaTime;

                if (energystop)
                    energycount = 0f;

                energystop = true;

                if (energy < 0)
                    energy = 0f;
            }

            if (isDashing)
            {
                dashC += Time.fixedDeltaTime;

                if (dashC > 0.5)
                {
                    isDashing = false;
                    ps.SetActive(false);
                    dashC = 0;
                }
            }
        }
        else if (isDead)
        {
                OnDeath.Invoke();
        }
    }

    private void FixedUpdate()
    {
        switch (estadoID)
        {
            case StateIds.Idle:

                cynthia.SetBool("XMove", false);
                cynthia.SetBool("isGrounded", true);
                cynthia.SetBool("Jump", false);
                cynthia.SetFloat("YSpeed", rb.velocity.y);
                cynthia.SetBool("Death", false);

                break;

            case StateIds.Walk:

                cynthia.SetBool("XMove", true);
                cynthia.SetBool("isGrounded", true);
                cynthia.SetBool("Jump", false);
                cynthia.SetFloat("YSpeed", rb.velocity.y);
                cynthia.SetBool("Death", false);

                break;

            case StateIds.Jump:

                cynthia.SetBool("YMove", true);
                cynthia.SetBool("isGrounded", false);
                cynthia.SetBool("Jump", true);
                cynthia.SetFloat("YSpeed", rb.velocity.y);
                cynthia.SetBool("Death", false);

                break;

            case StateIds.Fall:

                cynthia.SetBool("YMove", true);
                cynthia.SetBool("isGrounded", false);
                cynthia.SetBool("Jump", false);
                cynthia.SetFloat("YSpeed", rb.velocity.y);
                cynthia.SetBool("Death", false);

                break;

            case StateIds.Dead:

                cynthia.SetBool("YMove", false);
                cynthia.SetBool("XMove", false);
                cynthia.SetBool("isGrounded", false);
                cynthia.SetBool("Jump", false);
                cynthia.SetFloat("YSpeed", rb.velocity.y);
                cynthia.SetBool("Death", true);

                break;
        }

        if (!isDead)
        {
            if (isDead)
            {
                estadoID = StateIds.Dead;
            }
            else if (!isGrounded && rb.velocity.y > 0)
            {
                estadoID = StateIds.Jump;
            }
            else if (!isGrounded && rb.velocity.y < 0)
            {
                estadoID = StateIds.Fall;
            }
            else if (isGrounded && !Input.GetButton("Horizontal"))
            {
                estadoID = StateIds.Idle;
                canDash = true;
            }
            else if (isGrounded && Input.GetButton("Horizontal"))
            {
                estadoID = StateIds.Walk;
                canDash = true;
            }
            

            /*
            if (isInmortal)
            {
                counter += Time.deltaTime;

                if (counter > 1f)
                {
                    isInmortal = false;
                }
            }
            */

            

            // daño por caída
            /*
            if (rb.velocity.y > 0 || rb.velocity.y == 0)
            {
                if (fallcountdown > 1.6f)
                {
                    prehp -= 2;
                    Debug.Log("Daño salto 2");
                }
                else if (fallcountdown > 0.8f)
                {
                    prehp -= 1;
                    Debug.Log("Daño salto 1");
                }

                fallcountdown = 0f;
            }
            else if (rb.velocity.y < 0)
            {
                fallcountdown += Time.deltaTime;
            }
            */

            if (Input.GetButton("Horizontal"))
            {
                rb.velocity = new Vector2(Input.GetAxis("Horizontal") * (speed + acceleration * Time.fixedDeltaTime) * Time.fixedDeltaTime, rb.velocity.y);
                if (rb.velocity.x > 0)
                    player.transform.localScale = new Vector3 (-originalLocalScale, player.transform.localScale.y, player.transform.localScale.z);
                else if (rb.velocity.x < 0)
                    player.transform.localScale = new Vector3(originalLocalScale, player.transform.localScale.y, player.transform.localScale.z);
            }

            if (playerlight.intensity > 5000)
            {
                playerlight.intensity -= 70000 * Time.fixedDeltaTime;
            }
            else if (playerlight.intensity < 5000)
            {
                playerlight.intensity = 5000;
            }
            else if (playerlight.intensity == 5000 && !flashrdy)
            {
                flashrdy = true;
            }

            if (energystop)
            {
                energycount += Time.fixedDeltaTime;

                if (energycount >= 2f)
                {
                    energystop = false;
                    energycount = 0f;
                }
            }
            else if (!energystop && energy < 100)
            {
                energy += 30 * Time.fixedDeltaTime;
            }

            Vector3 desiredPosition = new Vector3(rb.position.x, rb.position.y, 0) + offset;
            Vector3 smoothedPosition = Vector3.Lerp(playerCamera.position, desiredPosition, smoothSpeed * Time.fixedDeltaTime);
            playerCamera.transform.position = smoothedPosition;

            if (Input.GetButtonDown("Flash") && lighthp > 0 && flashrdy)
            {
                OnFlash.Invoke();
            }

            energybar.localScale = new Vector3(energy / 100, 1f);
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (isInmortal && col.gameObject.tag == "Enemy")
        {
            Physics2D.IgnoreCollision(col.gameObject.GetComponent<CapsuleCollider2D>(), player.GetComponent<BoxCollider2D>());
        }
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(spotlight.transform.position, spotlight.transform.forward * detectionDistance);

        RaycastHit2D hit = Physics2D.Raycast(spotlight.transform.position, spotlight.transform.forward, 18, LayerMask.GetMask("Enemies"));
        Color col = hit.collider ? Color.green : Color.red;
        Gizmos.color = col;
        Gizmos.DrawRay(spotlight.transform.position, spotlight.transform.forward * 18);

    }
    #endregion

    void Light()
    {
        if (lighthp == 10)
        {
            hp10.enabled = true;
            hp9.enabled = true;
            hp8.enabled = true;
            hp7.enabled = true;
            hp6.enabled = true;
            hp5.enabled = true;
            hp4.enabled = true;
            hp3.enabled = true;
            hp2.enabled = true;
            hp1.enabled = true;
            cg.alpha = 0;
        }
        else if (lighthp == 9)
        {
            hp10.enabled = false;
            hp9.enabled = true;
            hp8.enabled = true;
            hp7.enabled = true;
            hp6.enabled = true;
            hp5.enabled = true;
            hp4.enabled = true;
            hp3.enabled = true;
            hp2.enabled = true;
            hp1.enabled = true;
            cg.alpha = 0;
        }
        else if (lighthp == 8)
        {
            hp10.enabled = false;
            hp9.enabled = false;
            hp8.enabled = true;
            hp7.enabled = true;
            hp6.enabled = true;
            hp5.enabled = true;
            hp4.enabled = true;
            hp3.enabled = true;
            hp2.enabled = true;
            hp1.enabled = true;
            cg.alpha = 0;
        }
        else if (lighthp == 7)
        {
            hp10.enabled = false;
            hp9.enabled = false;
            hp8.enabled = false;
            hp7.enabled = true;
            hp6.enabled = true;
            hp5.enabled = true;
            hp4.enabled = true;
            hp3.enabled = true;
            hp2.enabled = true;
            hp1.enabled = true;
            cg.alpha = 0;
        }
        else if (lighthp == 6)
        {
            hp10.enabled = false;
            hp9.enabled = false;
            hp8.enabled = false;
            hp7.enabled = false;
            hp6.enabled = true;
            hp5.enabled = true;
            hp4.enabled = true;
            hp3.enabled = true;
            hp2.enabled = true;
            hp1.enabled = true;
            cg.alpha = 0;
        }
        else if (lighthp == 5)
        {
            hp10.enabled = false;
            hp9.enabled = false;
            hp8.enabled = false;
            hp7.enabled = false;
            hp6.enabled = false;
            hp5.enabled = true;
            hp4.enabled = true;
            hp3.enabled = true;
            hp2.enabled = true;
            hp1.enabled = true;
            cg.alpha = 0;
        }
        else if (lighthp == 4)
        {
            hp10.enabled = false;
            hp9.enabled = false;
            hp8.enabled = false;
            hp7.enabled = false;
            hp6.enabled = false;
            hp5.enabled = false;
            hp4.enabled = true;
            hp3.enabled = true;
            hp2.enabled = true;
            hp1.enabled = true;
            cg.alpha = 0;
        }
        else if (lighthp == 3)
        {
            hp10.enabled = false;
            hp9.enabled = false;
            hp8.enabled = false;
            hp7.enabled = false;
            hp6.enabled = false;
            hp5.enabled = false;
            hp4.enabled = false;
            hp3.enabled = true;
            hp2.enabled = true;
            hp1.enabled = true;
            cg.alpha = 0;
        }
        else if (lighthp == 2)
        {
            hp10.enabled = false;
            hp9.enabled = false;
            hp8.enabled = false;
            hp7.enabled = false;
            hp6.enabled = false;
            hp5.enabled = false;
            hp4.enabled = false;
            hp3.enabled = false;
            hp2.enabled = true;
            hp1.enabled = true;
            cg.alpha = 0;
        }
        else if (lighthp == 1)
        {
            hp10.enabled = false;
            hp9.enabled = false;
            hp8.enabled = false;
            hp7.enabled = false;
            hp6.enabled = false;
            hp5.enabled = false;
            hp4.enabled = false;
            hp3.enabled = false;
            hp2.enabled = false;
            hp1.enabled = true;
            cg.alpha = 0;
        }
        else if (lighthp == 0)
        {
            hp10.enabled = false;
            hp9.enabled = false;
            hp8.enabled = false;
            hp7.enabled = false;
            hp6.enabled = false;
            hp5.enabled = false;
            hp4.enabled = false;
            hp3.enabled = false;
            hp2.enabled = false;
            hp1.enabled = false;
            cg.alpha = 1f;
        }
    }

    /// <summary>
    /// Regenera al personaje en el mundo, los valores del personaje vuelven a su estado inicial 
    /// </summary>
    /// <param name="Respawn"></param>
    public void Respawn (Vector3 respawnPoint)
    {
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponentInChildren<CircleCollider2D>().enabled = true;
        transform.position = respawnPoint;
        prehp = 10;
        energy = 100;
        playerlight.enabled = true;

        
    }

    public void Death()
    {
        if (playerlight)
        {
            playerlight.enabled = false;
        }
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponentInChildren<CircleCollider2D>().enabled = false;
    }

    public void Jump()
    {
        rb.AddForce(new Vector2(0, 32), ForceMode2D.Impulse);

        energy -= 5f;

        if (energystop)
            energycount = 0f;

        energystop = true;

        if (energy < 0)
            energy = 0f;

        isGrounded = false;
    }

    public void Flash()
    {
        int layer_mask = LayerMask.GetMask("Enemies");

        RaycastHit2D hit = Physics2D.Raycast(spotlight.transform.position, spotlight.transform.forward, detectionDistance, layer_mask);
        //Color col = hit.collider ? Color.green : Color.red;
        //Debug.DrawRay(spotlight.transform.position, spotlight.transform.forward * detectionDistance, col);
        //Debug.Log(hit.collider.gameObject.name);

        playerlight.intensity = 75000;
        prehp--;
        flashrdy = false;

        if (hit)
        {
            if (hit.collider.tag == "Enemy")
            {
                Transform simpleEnemy = hit.transform;
                simpleEnemy.GetComponentInChildren<SimpleEnemy>().hp--;
            }
        }
    }

    public void Dash()
    {
        mousePos = Input.mousePosition;
        Vector3 screenCentre = new Vector3((float)Screen.width / 2.0f, (float)Screen.height / 2.0f, 0);
        Vector3 result = Input.mousePosition - screenCentre;
        result.Normalize();
        //Debug.Log("Resultado: " + result);

        ps.SetActive(true);

        energy -= 20f;

        if (energystop)
            energycount = 0f;

        energystop = true;

        if (energy < 0)
            energy = 0f;

        int dash_layer = LayerMask.GetMask("Default");

        RaycastHit2D collision = Physics2D.Raycast(spotlight.transform.position, spotlight.transform.forward, 10f, dash_layer);

        //Debug.Log(collision.point);
        //Debug.Log(collision.collider.gameObject.name);

        float distance;

        if (collision)
        {
            distance = Vector2.Distance(new Vector2(spotlight.transform.position.x, spotlight.transform.position.y), new Vector2(collision.point.x, collision.point.y));
        }
        else
        {
            distance = 10f;
        }

        //Debug.Log(distance);

        float dash;

        if (distance < 10)
        {
            dash = distance;
        }
        else
        {
            dash = 10f;
        }

        //Debug.Log(dash);

        rb.position += new Vector2(dash * result.x, dash * result.y);

        canDash = false;
        isDashing = true;
    }

    public bool RemoveKey(int KeysToRemove = 1)
    {
        bool hasKeys = key >= KeysToRemove;
        if (!hasKeys)
            return false;

        //añadir efecto de sonido cuando se quita una llave

        key -= KeysToRemove;
        return true;
    }

    public bool RemoveRune(int RunesToRemove = 1)
    {
        bool hasRunes = runes >= RunesToRemove;
        if (!hasRunes)
            return false;

        //añadir efecto de sonido cuando se quita una llave

        runes -= RunesToRemove;
        return true;
    }
}