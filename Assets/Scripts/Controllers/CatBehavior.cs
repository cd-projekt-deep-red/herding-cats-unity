using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CatBehavior : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private Animator animator;
    private CatBehaviorState state = CatBehaviorState.Sitting;
    private Vector2 destination;

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
            Vector3 newPosition = Vector3.MoveTowards(this.transform.position, this.destination, Time.fixedDeltaTime);
            this.rigidBody.MovePosition(newPosition);
            if (((Vector2)this.transform.position - this.destination).magnitude < 0.01)
            {
                this.state = CatBehaviorState.Sitting;
            }
        }
    }

    private void LateUpdate()
    {
        switch (this.state)
        {
            case CatBehaviorState.Moving:
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
            Array values = Enum.GetValues(typeof(CatBehaviorState));
            CatBehaviorState newState = (CatBehaviorState) values.GetValue(Random.Range(0, values.Length));
            if (newState == CatBehaviorState.Moving)
            {
                this.destination = new Vector2(Random.Range(-10f, 10f), Random.Range(-10f, 10f));
            }
            this.state = newState;
        }
    }
}

public enum CatBehaviorState
{
    Moving,
    Sitting,
    Standing
}
