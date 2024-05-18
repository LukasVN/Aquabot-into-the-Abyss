using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float sprintSpeed = 7.5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Slider cooldownSlider;
    public SpriteRenderer blackScreen;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Rigidbody2D rb;
    private bool isGrounded;
    private float groundCheckRadius = 0.3f;
    public float recoilForce = 7.5f;
    private float startSpeed;
    private float habilityCooldown;
    public Transform spawnpoint;
    public float fadeDuration = 1f;
    public bool respawning;

    void Start()
    {
        //Just in case
        Color blackColor = blackScreen.color;
        blackColor.a = 0f;
        blackScreen.color = blackColor;
        blackScreen.gameObject.SetActive(false);
        //
        respawning = false;
        cooldownSlider.value = 0;
        habilityCooldown = 0;
        startSpeed = moveSpeed;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void OnDrawGizmosSelected()
    {
        // Draw a wire sphere to visualize the ground check area
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    void Update()
    {       
        if(respawning){
            return;
        }
        // Check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Player movement
        float moveInput = Input.GetAxis("Horizontal");
        spriteRenderer.flipX = moveInput < 0;

        if(Input.GetKey(KeyCode.LeftShift) && isGrounded){
            moveSpeed = sprintSpeed;
            animator.speed = 2f;
        }
        else if(moveSpeed == sprintSpeed && isGrounded){
            moveSpeed = startSpeed;
            animator.speed = 1f;
        }

        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if(isGrounded){
            animator.SetBool("isFalling",false);
        }

        if(moveInput != 0 && isGrounded){
            
            // animator.SetBool("isJumping",false); 
            animator.SetBool("isIdle",false);
            animator.SetBool("isIdle",false);
            animator.SetBool("isWalking",true);
        }
        else{
            animator.SetBool("isWalking",false);       
        }
        if (Mathf.Abs(moveInput) < 0.01f && isGrounded && !animator.GetBool("isJumping"))
        {
            animator.SetBool("isWalking",false);   
            // animator.SetBool("isJumping",false); 
            animator.SetBool("isIdle",true);
        }
    

        // Player jump
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetBool("isWalking",false); 
            animator.SetBool("isIdle",false);
            animator.SetBool("isJumping",true);  
            animator.SetBool("isFalling",false);  
        }
        else{
            animator.SetBool("isJumping",false);  
        }

        if(!animator.GetBool("isJumping") && !isGrounded){
            animator.SetBool("isWalking",false);
            animator.SetBool("isIdle",false);
            animator.SetBool("isFalling",true);  
        }

        if(habilityCooldown > 0){
            habilityCooldown -= Time.deltaTime;
            cooldownSlider.value = habilityCooldown;
            return;
        }

        if(Input.GetKeyDown(KeyCode.UpArrow)){
            rb.gravityScale = 0;
            if(!isGrounded && rb.velocity.y < 0){
                rb.AddForce(Vector2.down * recoilForce * 2,ForceMode2D.Impulse);
            }
            else{
                rb.AddForce(Vector2.down * recoilForce/2,ForceMode2D.Impulse);
            }
            StartCoroutine("ReenableGravity");
            habilityCooldown = 1f;
            cooldownSlider.value = habilityCooldown;
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow)){
            rb.gravityScale = 0;
            if(!isGrounded && rb.velocity.y < 0){
                rb.AddForce(Vector2.up * recoilForce * 2,ForceMode2D.Impulse);
            }
            else{
                rb.AddForce(Vector2.up * recoilForce/2,ForceMode2D.Impulse);
            }
            StartCoroutine("ReenableGravity");
            habilityCooldown = 1f;
            cooldownSlider.value = habilityCooldown;
        }

        


    }

    private IEnumerator ReenableGravity() {
        // Wait for a short period to allow the force to be applied without gravity
        yield return new WaitForSeconds(0.1f);

        // Re-enable gravity
        rb.gravityScale = 1.8f;
    }

    public void DeathReset(){
        rb.velocity = Vector2.zero;
        StartCoroutine("FadeTransition");
    }

    public void SetSpawnpoint(Transform newSpawnpoint){
        spawnpoint = newSpawnpoint;
    }

    #region Transitions

    private IEnumerator FadeTransition()
    {
        respawning = true;
        cooldownSlider.value = 0;
        habilityCooldown = 0f;
        // Fade out player sprite
        yield return StartCoroutine(FadeOut(spriteRenderer, fadeDuration));

        // Activate and fade in black screen
        blackScreen.gameObject.SetActive(true);
        yield return StartCoroutine(FadeIn(blackScreen, fadeDuration));
        transform.position = spawnpoint.position;
        // Perform respawn or other actions here

        // Optionally fade out the black screen again
        yield return StartCoroutine(FadeOut(blackScreen, fadeDuration));
        blackScreen.gameObject.SetActive(false);

        // Optionally fade in the player sprite again
        yield return StartCoroutine(FadeInPlayer(spriteRenderer, fadeDuration));
        respawning = false;
    }

    private IEnumerator FadeOut(SpriteRenderer spriteRenderer, float duration)
    {
        Color color = spriteRenderer.color;
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            color.a = Mathf.Lerp(1f, 0f, t / duration);
            spriteRenderer.color = color;
            yield return null;
        }
        color.a = 0f;
        spriteRenderer.color = color;
    }

    private IEnumerator FadeIn(SpriteRenderer spriteRenderer, float duration)
    {
        Color color = spriteRenderer.color;
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            color.a = Mathf.Lerp(0f, 1f, t / duration);
            spriteRenderer.color = color;
            yield return null;
        }
        color.a = 1f;
        spriteRenderer.color = color;
    }

    private IEnumerator FadeInPlayer(SpriteRenderer spriteRenderer, float duration)
    {
        Color color = spriteRenderer.color;
        float halfFadeDuration = duration / 4;
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            color.a = Mathf.Lerp(0f, 1f, t / duration);
            spriteRenderer.color = color;
            if (t >= halfFadeDuration)
            {
                respawning = false;
            }
            yield return null;
        }
        color.a = 1f;
        spriteRenderer.color = color;
    }
    #endregion

}
