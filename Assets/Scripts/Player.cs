using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerSpriteRenderer smallRenderer; 
    public PlayerSpriteRenderer bigRenderer;
    private PlayerSpriteRenderer activeRenderer; //Lưu giữ renderer hiện tại đang hoạt động.
    private DeathAnimation deathAnimation;
    private CapsuleCollider2D capsuleCollider;
    public bool big => bigRenderer.enabled;
    public bool small => smallRenderer.enabled;
    public bool dead => deathAnimation.enabled;
    public bool starpower {get; private set; }
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        deathAnimation = GetComponent<DeathAnimation>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        activeRenderer = smallRenderer;
    }
    public void Hit () //bi tan cong
    {
        if (!dead && !starpower) 
        {        
        if (big)
        {
            Shrink();
        }
        else
        {
            Death();
        }
        }
    }

    private void Death()
    {
        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        deathAnimation.enabled = true;
        audioManager.PlaySFX(audioManager.MarioDeath);

        GameManager.Instance.ResetLevel(3f);
    }
    public void Grow() //lon len
    {
        smallRenderer.enabled = false;
        bigRenderer.enabled = true;
        activeRenderer = bigRenderer;
        audioManager.PlaySFX(audioManager.MarioEaten);

        capsuleCollider.size = new Vector2(1f, 2f);
        capsuleCollider.offset = new Vector2(0f, 0.5f);
         
        StartCoroutine(ScaleAnimation());
    }

    private void Shrink() //nho lai
    {
        smallRenderer.enabled = true;
        bigRenderer.enabled = false;
        activeRenderer = smallRenderer;
        audioManager.PlaySFX(audioManager.Pipe);

        capsuleCollider.size = new Vector2(1f, 1f);
        capsuleCollider.offset = new Vector2(0f, 0f);
        StartCoroutine(ScaleAnimation());
    }

    private IEnumerator ScaleAnimation() //hien thi small - big luan phien
    {
        float elapsed = 0f;
        float duration = 0.5f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            if (Time.frameCount % 4 == 0 )
            {
                smallRenderer.enabled = !smallRenderer.enabled;
                bigRenderer.enabled = !smallRenderer.enabled;
            }

            yield return null;
        }

        smallRenderer.enabled = false ;
        bigRenderer.enabled = false;
        activeRenderer.enabled = true ; 
    }
    public void Starpower(float duration = 10f) //bat tu 10s
    {
        StartCoroutine(StarpowerAnimation(duration));
    }

    private IEnumerator  StarpowerAnimation(float duration)
    {
        starpower = true;
        audioManager.PlaySFX(audioManager.MarioEaten);

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            if (Time.frameCount % 4 == 0) // 4 khung hinh 1 lan
            {
                activeRenderer.spriteRenderer.color =  Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
            }

            yield return null; // Tạm dừng coroutine cho đến khung hình tiếp theo.
        }

        activeRenderer.spriteRenderer.color = Color.white; 
        starpower = false;
        audioManager.PlaySFX(audioManager.Pipe);
    }
}
