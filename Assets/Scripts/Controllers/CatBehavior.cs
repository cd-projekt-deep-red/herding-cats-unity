using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using Random = UnityEngine.Random;

public class CatBehavior : MonoBehaviour
{
    public float stamina;
    public float staminaRecoverSpeed;
    public float maxStamina;
    public float speed;
    public float fondness;
    public CatBehaviorState state;
    public Vector2 destination;
    public Vector2 antiDestination;

    private Rigidbody2D rigidBody;
    private Animator animator;

    void Start()
    {
        this.rigidBody = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        StartCoroutine("CycleState");
    }

    void FixedUpdate()
    {
        if (this.state == CatBehaviorState.Moving || this.state == CatBehaviorState.Eating)
        {
            // https://forum.unity.com/threads/rigidbody-moveposition-doesnt-stop-moving-even-after-reaching-destination.544552/#post-3591916
            Vector3 newPosition = Vector3.MoveTowards(this.transform.position, this.destination, Time.fixedDeltaTime * speed);
            this.rigidBody.MovePosition(newPosition);
            if (((Vector2)this.transform.position - this.destination).magnitude < 0.5f)
            {
                if (this.state == CatBehaviorState.Eating)
                {
                    StartCoroutine("EatAndCycleState");
                }
                else
                {
                    this.state = CatBehaviorState.Sitting;
                }
            }
        }
        else if (this.state == CatBehaviorState.WalkAway)
        {
            this.fondness -= .01f;
            Vector3 newPosition = Vector3.MoveTowards(this.transform.position, (Vector2)this.transform.position * 2 - this.antiDestination, Time.fixedDeltaTime * speed);
            this.rigidBody.MovePosition(newPosition);
            if (((Vector2)this.transform.position - this.antiDestination).magnitude > 2)
            {
                StartCoroutine("CycleState");
            }
        }
        else if (this.state == CatBehaviorState.RunAway)
        {
            this.fondness -= .05f;
            Vector3 newPosition = Vector3.MoveTowards(this.transform.position, (Vector2)this.transform.position * 2 - this.antiDestination, Time.fixedDeltaTime * speed * 3);
            this.rigidBody.MovePosition(newPosition);
            if (((Vector2)this.transform.position - this.antiDestination).magnitude > 5)
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
            case CatBehaviorState.Eating:
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
            System.Collections.Generic.IEnumerable<CatBehaviorState> values = Enum.GetValues(typeof(CatBehaviorState))
                .Cast<CatBehaviorState>()
                .Where(s =>
                    s != CatBehaviorState.WalkAway &&
                    s != CatBehaviorState.RunAway &&
                    s != CatBehaviorState.Eating);
            CatBehaviorState newState = (CatBehaviorState) values.ElementAt(Random.Range(0, values.Count()));
            if (newState == CatBehaviorState.Moving)
            {
                this.destination = new Vector2(Random.Range(-20f, 20f), Random.Range(-20f, 20f));
            }
            this.state = newState;
            yield return new WaitForSeconds(Random.Range(1f, 5f));
        }
    }

    private IEnumerator EatAndCycleState()
    {
        yield return new WaitForSeconds(Random.Range(5f, 5f));
        // I'm done eating
        StartCoroutine("CycleState");
    }

    public void OnPlayerDetected(GameObject player)
    {
        if (this.fondness <= 1 &&
            !player.GetComponent<PlayerOne>().isCrouching &&
            this.state != CatBehaviorState.Eating)
        {
            if (this.fondness >= -10 &&
                (this.transform.position - player.transform.position).magnitude > 2)
            {
                StopCoroutine("CycleState");
                this.state = CatBehaviorState.WalkAway;
                this.antiDestination = player.transform.position;
            }
            else
            {
                StopCoroutine("CycleState");
                this.state = CatBehaviorState.RunAway;
                this.antiDestination = player.transform.position;
            }
        }
    }

    public void OnFoodDetected(GameObject food)
    {
        StopCoroutine("CycleState");
        this.state = CatBehaviorState.Eating;
        this.destination = food.transform.position;
    }
}

public enum CatBehaviorState
{
    Moving,
    Sitting,
    Standing,
    WalkAway,
    RunAway,
    Eating
}
