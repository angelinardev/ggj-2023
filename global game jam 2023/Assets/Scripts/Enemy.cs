using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // states
    // sleeping/not moving/not aggro'd
    // Woken, but not agroo'd just moving around
    // Aggro'd and actively moving to player

    enum States
    {
        sleep, //0
        roaming, //1
        aggo // 2
    }

    public float moveSpeed;
    public float maxRoamTime;

    public float maxSeeDistanceForCollision;

    private Rigidbody rb;
    private States state;

    private Transform player;
    private Transform playerPosition;

    private bool active = false;
    private float activeTimer = 0f;

    private float timeToTurn = 3.738f;
    private float maxTurn = 89f;
    private float facing = 0;

    private float timeToUpdate = 2f;

    private Vector3 targetDirection;

    private bool roaming;
    private bool chasing;

    private CollectingInventory collectInstance;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        //state = States.sleep;
        player = null;
        state = States.roaming;
        active = true;

        collectInstance = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CollectingInventory>();

        StartCoroutine(Roaming());
    }

    private void Update()
    {
        //transform.eulerAngles = new Vector3(0, transform.rotation.y, 0);


        {
            Vector3 temp = new Vector3(0, transform.eulerAngles.y);
            transform.eulerAngles = temp;
        }

        if (player != null && active) state = States.aggo;
        if (player == null && active) state = States.roaming;
        if (!active) state = States.sleep;

        if (state == States.sleep) if (!active) return; else state = States.aggo;
        if (state == States.roaming)
        {
            if (!roaming) StartCoroutine(Roaming());
            Roam();
        }
        if (state == States.aggo)
        {
            if (!chasing) StartCoroutine(Chasing());
            Chase();
        }
    }

    private void Sleep()
    {
        state = States.sleep;
        ResetChase(); ResetActive();
    }

    private void Roam()
    {
        ResetChase();
        // TODO: Avoid collisions

        // raycast forward, if it hits then we turn
        RaycastHit hit;

        if(Physics.SphereCast(transform.position + (transform.forward), 2f, transform.forward, out hit, maxSeeDistanceForCollision))
        {
            if (hit.transform.CompareTag("Environment"))
            {
                ChangeDirection();
            }
        }


        if (activeTimer >= maxRoamTime) { active = false; Sleep(); }

        transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, targetDirection, Time.deltaTime * timeToTurn);

        rb.velocity = transform.forward * moveSpeed;
        activeTimer += Time.deltaTime;
    }

    private void ChangeDirection()
    {
        facing = Random.Range(Mathf.Clamp(facing - maxTurn, 0, 360), Mathf.Clamp(facing + maxTurn, 0, 360));
        targetDirection = new Vector3(0, facing, 0);
    }

    private void Chase()
    {
        ResetActive();

        if(playerPosition != null)
        transform.LookAt(player);

        playerPosition = null;
        rb.velocity = transform.forward * moveSpeed * 1.34f;

    }

    private void UpdatePlayerPosition()
    {
        if (player == null) {state = States.roaming; return; }
        playerPosition = player;
        playerPosition.position = new Vector3(playerPosition.position.x, transform.position.y, playerPosition.position.z);
        transform.LookAt(playerPosition);
        //targetDirection = playerPosition.position - transform.position;
    }

    private void ResetActive()
    {
        activeTimer = 0;
        StopCoroutine(Roaming());
        roaming = false;
    }

    private void ResetChase()
    {
        StopCoroutine(Chasing());
        chasing = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) active = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
            player = other.transform;
    }

    private void OnTriggerExit(Collider other)
    {
        ResetChase();
        player = null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if (collision.transform.childCount > 2)
            {
                collectInstance.SetCollected(-1);
                collision.transform.GetChild(2).parent = null;
            }

            //knock back or something for later
            rb.AddForce((transform.position - collision.transform.position) * 50, ForceMode.Impulse) ;
            Invoke("Restart", 2.73475f);
            enabled = false;
        }
    }

    IEnumerator Roaming()
    {
        while (true)
        {
            roaming = true;
            //if (justTurned) yield return new WaitForSeconds(timeToUpdate);
            //justTurned = false;
            ChangeDirection();
            yield return new WaitForSeconds(timeToTurn);
        }
    }

    IEnumerator Chasing()
    {
        while (true)
        {
            chasing = true;
            UpdatePlayerPosition();
            yield return new WaitForSeconds(timeToUpdate);

        }
    }

    public void Restart()
    {
        enabled = true;
    }
}
