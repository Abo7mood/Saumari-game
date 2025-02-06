
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBarFade : MonoBehaviour
{
    PlayerController playermovement;
    private const float DAMAGED_HEALTH__fade_TIMER_MAX = .6f;
    public float MaxHealth = 100f;
    private Image barImage;
    private Image damagedBarImage;
    private Color damagedColor;
    private float damagedHealth_fadeTimer;
    private HealthSystem healthSystem;

    private void Awake()
    {
     
        healthSystem = GetComponent<HealthSystem>();
        barImage = transform.Find("Fade").GetComponent<Image>();
        damagedBarImage = transform.Find("BackGround").GetComponent<Image>();
        damagedColor = damagedBarImage.color;
        damagedColor.a = 0f;
        damagedBarImage.color = damagedColor;
      
    }

    private void Start()
    {

        healthSystem.SetUp(MaxHealth);
        SetHealth(healthSystem.GetHealthNormalized());
        healthSystem.OnDamaged += HealthSystem_OnDamaged;
        healthSystem.OnHealed += HealthSystem_OnHealed;
        Invoke(nameof(PlayerReference), LevelManager.instance.RespawnTime+0.01f);

    }

    private void Update()
    {

        if (damagedColor.a > 0)
        {
            damagedHealth_fadeTimer -= Time.deltaTime;
            if (damagedHealth_fadeTimer < 0)
            {
                float _fadeAmount = 5f;
                damagedColor.a -= _fadeAmount * Time.deltaTime;
                damagedBarImage.color = damagedColor;
            }
        }
    }

    private void HealthSystem_OnHealed(object sender, System.EventArgs e)
    {
        SetHealth(healthSystem.GetHealthNormalized());
    }

    private void HealthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        if (damagedColor.a <= 0)
        {
            // Damaged bar image is invisible
            damagedBarImage.fillAmount = barImage.fillAmount;
        }
        damagedColor.a = 1;
        damagedBarImage.color = damagedColor;
        damagedHealth_fadeTimer = DAMAGED_HEALTH__fade_TIMER_MAX;

        SetHealth(healthSystem.GetHealthNormalized());
    }

    private void SetHealth(float healthNormalized)
    {
        barImage.fillAmount = healthNormalized;
        if (this.healthSystem.healthAmount <= 0&&playermovement._isDead==false)
            playermovement.Dead();      
    }
    public void Damage(int SetDamage)
    {
        healthSystem.Damage(SetDamage);
    }
    public void Health(int SetHealth)
    {
        healthSystem.Heal(SetHealth);
    }
    private void PlayerReference() => playermovement = FindObjectOfType<PlayerController>();
}
