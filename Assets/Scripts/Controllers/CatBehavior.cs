using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class CatBehavior : MonoBehaviour
{
    public float stamina;
    public float staminaRecoverSpeed;
    public float maxStamina;
    public float speed;
    public float fondness;
    private Rigidbody2D rigidBody;
    private Animator animator;
    private CatBehaviorState state;
    private Vector2 destination;
    private Vector2 antiDestination;

    void Start()
    {
        this.rigidBody = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        StartCoroutine("CycleState");
    }

    void FixedUpdate()
    {
        if (this.state == CatBehaviorState.Moving)
        {
            // https://forum.unity.com/threads/rigidbody-moveposition-doesnt-stop-moving-even-after-reaching-destination.544552/#post-3591916
            //Vector3 newPosition = Vector3.MoveTowards(this.transform.position, this.destination, Time.fixedDeltaTime * speed);
            Vector3 newPosition = Vector3.MoveTowards(this.transform.position, this.destination, Time.fixedDeltaTime * speed);
            this.rigidBody.MovePosition(newPosition);
            if (((Vector2)this.transform.position - this.destination).magnitude < 0.01)
            {
                this.state = CatBehaviorState.Sitting;
            }
        }
        else if (this.state == CatBehaviorState.WalkAway)
        {
            this.fondness -= .1f;
            Vector3 newPosition = Vector3.MoveTowards(this.transform.position, (Vector2)this.transform.position * 2 - this.antiDestination, Time.fixedDeltaTime * speed * 2);
            this.rigidBody.MovePosition(newPosition);
            if (((Vector2)this.transform.position - this.antiDestination).magnitude > 10)
            {
                StartCoroutine("CycleState");
            }
        }
        else if (this.state == CatBehaviorState.RunAway)
        {
            this.fondness -= .1f;
            Vector3 newPosition = Vector3.MoveTowards(this.transform.position, (Vector2)this.transform.position * 2 - this.antiDestination, Time.fixedDeltaTime * speed * 6);
            this.rigidBody.MovePosition(newPosition);
            if (((Vector2)this.transform.position - this.antiDestination).magnitude > 10)
            {
                StartCoroutine("CycleState");
            }
        }
    }

    private void LateUpdate()
    {
        switch (this.state)
        {
            case CatBehaviorState.Moving:
            case CatBehaviorState.WalkAway:
            case CatBehaviorState.RunAway:
                if (this.rigidBody.velocity.x > 0)
                {
                    this.animator.SetBool("MoveRight", true);
                    this.animator.SetBool("MoveLeft", false);
                    this.animator.SetBool("Sit", false);
                }
                else
                {
                    this.animator.SetBool("MoveRight", false);
                    this.animator.SetBool("MoveLeft", true);
                    this.animator.SetBool("Sit", false);
                }
                break;
            case CatBehaviorState.Sitting:
                this.animator.SetBool("MoveRight", false);
                this.animator.SetBool("MoveLeft", false);
                this.animator.SetBool("Sit", true);
                break;
            case CatBehaviorState.Standing:
                this.animator.SetBool("MoveRight", false);
                this.animator.SetBool("MoveLeft", false);
                this.animator.SetBool("Sit", false);
                break;
            default:
                break;
        }
    }

    private IEnumerator CycleState()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1f, 5f));
            System.Collections.Generic.IEnumerable<CatBehaviorState> values = Enum.GetValues(typeof(CatBehaviorState))
                .Cast<CatBehaviorState>()
                .Where(s => s != CatBehaviorState.WalkAway && s != CatBehaviorState.RunAway);
            CatBehaviorState newState = (CatBehaviorState) values.ElementAt(Random.Range(0, values.Count()));
            if (newState == CatBehaviorState.Moving)
            {
                this.destination = new Vector2(Random.Range(-20f, 20f), Random.Range(-20f, 20f));
            }
            this.state = newState;
        }

    }

    public void OnPlayerDetected(GameObject player)
    {
        if (this.fondness <= 1 && this.fondness >= -10)
        {
            StopCoroutine("CycleState");
            this.state = CatBehaviorState.WalkAway;
            this.antiDestination = player.transform.position;
        }
        else if (this.fondness < -10)
        {
            StopCoroutine("CycleState");
            this.state = CatBehaviorState.RunAway;
            this.antiDestination = player.transform.position;
        }
    }
}

public enum CatBehaviorState
{
    Moving,
    Sitting,
    Standing,
    WalkAway,
    RunAway
}
