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
    public bool isBeingChased = false;

    private Rigidbody2D rigidBody;
    private Animator animator;
    [SerializeField]private Animator emoter;
    [SerializeField]private GameObject dustPrefab;
    [SerializeField]private float dustSpawnDelay = 0.06f;
    [SerializeField]private float dustSpawnVariance = 0.03f;
    [SerializeField]private RectTransform emoteTransform;
    [SerializeField]private AudioSource audioSource;
    [SerializeField]private AudioClip[] angryMeow;
    [SerializeField]private AudioClip[] happyMeow;
    private SpriteRenderer spriteRenderer;
    private float dustSpawned = 0f;
    private Vector2 previousPosition;
    private Vector2 velocity;

    void Start()
    {
        this.rigidBody = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        StartCoroutine("CycleState");
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        var currentPosition = (Vector2)this.transform.position;
        this.velocity = currentPosition - previousPosition;
        if (this.state == CatBehaviorState.Moving || this.state == CatBehaviorState.MovingToFood)
        {
            // https://forum.unity.com/threads/rigidbody-moveposition-doesnt-stop-moving-even-after-reaching-destination.544552/#post-3591916
            Vector2 newPosition = Vector2.MoveTowards(currentPosition, this.destination, Time.fixedDeltaTime * speed);
            this.rigidBody.MovePosition(newPosition);
            if ((currentPosition - this.destination).magnitude < 0.5f)
            {
                if (this.state == CatBehaviorState.Moving)
                {
                    this.state = CatBehaviorState.Sitting;
                } else if (this.state == CatBehaviorState.MovingToFood)
                {
                    this.state = CatBehaviorState.Eating;
                }
            }

            // Reset the being chased bool
            if(isBeingChased)
            {
              isBeingChased = false;
            }
        }
        else if (this.state == CatBehaviorState.WalkAway)
        {
            this.fondness -= .01f;
            Vector2 newPosition = Vector2.MoveTowards(currentPosition, currentPosition * 2 - this.antiDestination, Time.fixedDeltaTime * speed);
            this.rigidBody.MovePosition(newPosition);
            if ((currentPosition - this.antiDestination).magnitude > 2 || this.velocity.magnitude == 0)
            {
                StartCoroutine("CycleState");
            }
        }
        else if (this.state == CatBehaviorState.RunAway)
        {
            // If the cat has just started being chased
            if(!isBeingChased)
            {
              isBeingChased = true;
              // 75% chance to make a noise
              if(Random.value > 0.25)
              {
                audioSource.pitch = Random.Range(0.9f, 2.0f);
                audioSource.PlayOneShot(angryMeow[Random.Range(0, angryMeow.Length-1)]);
              }
            }
            this.fondness -= .05f;
            var runDirection = (currentPosition - this.antiDestination).normalized;
            Vector2 newPosition = Vector2.MoveTowards(currentPosition, currentPosition + runDirection * 10, Time.fixedDeltaTime * speed * 3);
            this.rigidBody.MovePosition(newPosition);

            // Add dust at interval
            if (dustSpawned <= 0.0f)
            {
              dustSpawned = Random.Range(dustSpawnDelay - dustSpawnVariance, dustSpawnDelay + dustSpawnVariance);
              // Spawn dust
              Vector2 dustLocation = currentPosition + new Vector2(0.0f, -.375f);
              GameObject dustParticle = Instantiate(dustPrefab, dustLocation, Quaternion.identity);
              if(this.velocity.x <= 0) {
                dustParticle.transform.localScale = new Vector2(-1f, 1f);
                emoteTransform.localPosition = new Vector2(-0.275f, 0.5f);
              }
              else
              {
                emoteTransform.localPosition = new Vector2(0.275f, 0.5f);
              }
            }
            else
            {
                dustSpawned = dustSpawned - Time.fixedDeltaTime;
            }

            if ((currentPosition - this.antiDestination).magnitude > 5 || this.velocity.magnitude == 0)
            {
                StartCoroutine("CycleState");
            }
        }


        spriteRenderer.sortingOrder = (int)(-1 * currentPosition.y + 150f);

        previousPosition = currentPosition;
    }

    private void LateUpdate()
    {
        switch (this.state)
        {
            case CatBehaviorState.Moving:
            case CatBehaviorState.WalkAway:
            case CatBehaviorState.RunAway:
                if (this.velocity.x > 0)
                {
                    this.animator.SetBool("MoveRight", true);
                    this.animator.SetBool("MoveLeft", false);
                    this.animator.SetBool("Sit", false);
                }
                else if (this.velocity.x < 0)
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
                // Reset the being chased bool
                if(isBeingChased)
                {
                  isBeingChased = false;
                }
                // Random chance to meow
                if(Random.value > 0.9992)
                {
                  audioSource.pitch = Random.Range(0.75f, 1.25f);
                  audioSource.volume = Random.Range(0.125f, 0.3f);
                  audioSource.PlayOneShot(happyMeow[Random.Range(0, happyMeow.Length)]);
                }
                break;
            case CatBehaviorState.Standing:
                this.animator.SetBool("MoveRight", false);
                this.animator.SetBool("MoveLeft", false);
                this.animator.SetBool("Sit", false);
                // Reset the being chased bool
                if(isBeingChased)
                {
                  isBeingChased = false;
                }
                break;
            default:
                break;
        }
        if (this.state == CatBehaviorState.RunAway)
        {
            this.emoter.SetBool("Suprise", true);
        }
        else
        {
            this.emoter.SetBool("Suprise", false);
        }
    }

    public void ClearEmotes()
    {
        this.emoter.SetBool("Suprise", false);
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
                    s != CatBehaviorState.MovingToFood &&
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
        yield return new WaitForSeconds(Random.Range(10f, 30f));
        // I'm done eating
        StartCoroutine("CycleState");
    }

    public void OnPlayerDetected(GameObject player)
    {
        if (this.fondness <= 1 &&
            !player.GetComponent<PlayerOne>().isCrouching &&
            this.state != CatBehaviorState.Eating &&
            this.state != CatBehaviorState.MovingToFood)
        {
            if (this.fondness >= -10 &&
                (this.transform.position - player.transform.position).magnitude > 2 &&
                this.state != CatBehaviorState.WalkAway &&
                this.state != CatBehaviorState.RunAway)
            {
                StopCoroutine("CycleState");
                this.state = CatBehaviorState.WalkAway;
                this.antiDestination = player.transform.position;
            }
            else if (this.state != CatBehaviorState.RunAway)
            {
                StopCoroutine("CycleState");
                this.state = CatBehaviorState.RunAway;
                this.antiDestination = player.transform.position;
            }
        }
    }

    public void OnFoodDetected(Food food)
    {
        if (!food.isBeingHeld)
        {
            if (this.state != CatBehaviorState.MovingToFood && this.state != CatBehaviorState.Eating)
            {
                StopCoroutine("CycleState");
                this.state = CatBehaviorState.MovingToFood;
                this.destination = food.transform.position;
                StartCoroutine("EatAndCycleState");
            }
            else if (this.state == CatBehaviorState.Eating)
            {
                food.Consume();
            }
        }
    }

    public void OnFoodRemoved(Food food)
    {
        if (this.state == CatBehaviorState.MovingToFood || this.state == CatBehaviorState.Eating)
        {
            StopCoroutine("EatAndCycleState");
            StartCoroutine("CycleState");
        }
    }
}

public enum CatBehaviorState
{
    Moving,
    Sitting,
    Standing,
    WalkAway,
    RunAway,
    Eating,
    MovingToFood
}
