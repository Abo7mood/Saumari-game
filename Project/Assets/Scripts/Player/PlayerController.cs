using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    #region Construct
    [Header("Construct")]
  

    [SerializeField] Collider2D _c1;
    [SerializeField] Collider2D _c2;
    Transform _backGround;
    [SerializeField] GroundCheck _groundCheck;
    HealthBarFade _fade;

    SpriteRenderer _mySprite;
    Rigidbody2D _rb;
    Animator _anim;
    BoxCollider2D _boxCollider;
    Renderer _rend;
    Joystick _joystick;
    Color _c;
    #endregion
    #region GameObjects
    GameObject _canvasTransform;
    GameObject DeadPanel;
    [SerializeField] GameObject _deadPanel;
    [SerializeField] GameObject _winPanel;
    [SerializeField] GameObject _deathParticle;
    [SerializeField] GameObject[] _healthImage;

    Button button;
    #endregion

    #region float&int
    [SerializeField] float _jump = 10f;
    public float _sprint;
    int _currentHealth = -1;
    [SerializeField] float _playerSpeed;
    float _movX;
    [SerializeField] float _playerHealth;
    #endregion

    #region bool
    bool _isRunning;
   static bool _isDamage;
    bool _canAttack = true, _canAttack2 = true;
    bool _isAttack = false;
    bool _canmove;
    public bool m_FacingRight;
    static bool _isHide;
    static bool _isGame;
    bool _jumpb = true;
    bool _isJump;
    bool _jumpsound;
    [HideInInspector] public bool _isDead = false;

    #endregion
    #region strings
    const string BACKGROUND = "BG";
    const string CANVASTRANSFORM = "PlayerCanvas";

    #endregion


    private void Awake()
    {
      
        _joystick = FindObjectOfType<Joystick>().GetComponent<Joystick>();
        //  _mySprite = GetComponentInChildren<SpriteRenderer>();
        _rb = transform.GetComponent<Rigidbody2D>();
        _anim = GetComponentInChildren<Animator>();
        _backGround = GameObject.Find(BACKGROUND).transform;
        _canvasTransform = GameObject.Find(CANVASTRANSFORM).gameObject;
        _fade = FindObjectOfType<HealthBarFade>();
        _boxCollider = transform.GetComponent<BoxCollider2D>();
        _rend = GetComponentInChildren<Renderer>();
        _c = _rend.material.color;
        DeadPanel = GameObject.Find("Static").transform.Find("Canvas").transform.Find("Dead").gameObject;
    }
    void Start()
    {
        _isDamage = false;
        Physics2D.IgnoreLayerCollision(8, 10, false);
       
        _jumpsound = true;
        _isDead = false;
        _isDamage = false;
        _isGame = true;
        _canmove = true;
        _fade.MaxHealth = _playerHealth;
        _fade.gameObject.GetComponent<HealthSystem>().healthAmount = _playerHealth;
       
    }

    // Update is called once per frame
    void Update()
    {
        animationfather();
        if (_canmove == true)
            PlayerMove();

        _backGround.transform.position = new Vector2(transform.position.x, transform.position.y);
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        if (_isAttack)
        {
            _rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            _rb.constraints = RigidbodyConstraints2D.None;
            _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        _canvasTransform.transform.localPosition = new Vector2(transform.position.x + -0.05499995f, transform.position.y + 0.5f);
    }



    public void ChangeHealth(int amount)
    {
        _currentHealth += amount;
        if (_healthImage[_currentHealth].gameObject != null)
            _healthImage[_currentHealth].gameObject.SetActive(false);
        else
            return;

    }
    private void PlayerMove()
    {
        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("runwarrior"))
        {
            SoundManager.instance.Walk();
        }
       

        _anim.SetFloat("Speed", (int)Mathf.Abs(_rb.velocity.x));

        if (!_isAttack)
        {
            if (_joystick.Horizontal >= .2f)
                _movX = _playerSpeed;
            else if (_joystick.Horizontal <= -.2f)
                _movX = -_playerSpeed;
            else
                _movX = 0;
        }
        float verticalMove = _joystick.Vertical;

        _rb.velocity = new Vector2(_movX * _playerSpeed * _sprint, _rb.velocity.y);
        if (_movX > 0 && !m_FacingRight)
            // ... flip the ==false	
            Flip();

        // Otherwise if the input is moving the player left and the player is facing right...	
        else if (_movX < 0 && m_FacingRight)
            // ... flip the player.	
            Flip();


        else
            _canmove = true;

        if (_joystick.Vertical >= .5f && _groundCheck.isGrounded == true)
        {
            _rb.velocity = new Vector2(0, _jump);
            if (_jumpsound == true)
            {
                StartCoroutine(JumpSoundCooldown());
                SoundManager.instance.JumpPlayerS();
            }
           
        }


        if (Mathf.Abs(_rb.velocity.y) < 0.001f)
            _isJump = false;

        //jumping
        if (_rb.velocity.y <= 0 && _rb.velocity.y > -0.5f)
        {
            _anim.SetBool("IsJumping", false);
            _anim.SetBool("IsFalling", false);
        }
        if (_rb.velocity.y > 0.01f)
        {
            _anim.SetBool("IsJumping", true);
        }

        if (_rb.velocity.y < -1.8)
        {
            _anim.SetBool("IsJumping", false);

            _anim.SetBool("IsFalling", true);
        }
        if (_rb.velocity.y >= 0)
            _anim.SetBool("IsFalling", false);

    }

    public void Dead()
    {
        _isDead = true;
        _anim.SetTrigger("Dead");

        _isDead = true;
        StopCoroutine(Hurt());
        StartCoroutine(Died());
        SoundManager.instance.DeadPlayerS();
        DeadPanel.SetActive(true);

      
        //mainlose

    }
    public void playerhealthdisable() => ChangeHealth(1);



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "E1" && _isDamage == false && _isDead == false)
        {
            Shaker.Instance.CamShaker(2f, 3f, .1f, .1f);
            SoundManager.instance.HitPlayer();
            _anim.Play("Hit");
            _fade.Damage(5);
            StartCoroutine(Hurt());
        }
        else if (collision.CompareTag("Spike") && _isDead == false)
        {
            SoundManager.instance.HitPlayer();
            Shaker.Instance.CamShaker(6f, 7f, .5f, .5f);
            _fade.Damage(1000);
            StartCoroutine(Hurt());
        }

        else if (collision.CompareTag("Sword") && _isDamage == false && _isDead == false)
        {
            SoundManager.instance.HitPlayer();
            Shaker.Instance.CamShaker(1f, 2f, .2f, .2f);

            _anim.Play("Hit");
            _fade.Damage(10);
            StartCoroutine(Hurt());
            collision.GetComponent<Animator>().SetTrigger("Attack");
            Destroy(collision, .3f);
        }
        else if (collision.CompareTag("Bomb") && _isDamage == false && _isDead == false)
        {
            SoundManager.instance.HitPlayer();
            Shaker.Instance.CamShaker(1f, 2f, .2f, .2f);

            _anim.Play("Hit");
            _fade.Damage(15);
            StartCoroutine(Hurt());
            collision.GetComponent<Animator>().SetTrigger("Attack");
            Destroy(collision, .3f);
        }

        else { return; }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && _isDamage == false && _isDead == false)
        {
          
            Shaker.Instance.CamShaker(3.5f, 4.5f, .1f, .1f);
            SoundManager.instance.HitPlayer();
            _anim.Play("Hit");
            _fade.Damage(1);
            StartCoroutine(Hurt());

        }
    }
    private void Flip()
    {
        // Switch the way the player is labelled as facing.	
        m_FacingRight = !m_FacingRight;
        transform.Rotate(0f, 180f, 0f);
    }
    IEnumerator Hurt()
    {
        _isDamage = true;
        _anim.ResetTrigger("Hit");
        Physics2D.IgnoreLayerCollision(8, 10);
      
        yield return new WaitForSeconds(2f);
        _isDamage = false;
        Physics2D.IgnoreLayerCollision(8, 10, false);
    }
    private bool IsGrounded()
    {
        return transform.Find("GroundCheck").GetComponent<GroundCheck>().isGrounded;
    }
    public void PlayerWin()
    {

    }
    private void animationfather()
    {
        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("attack1").Equals(true)
            || _anim.GetCurrentAnimatorStateInfo(0).IsName("attack2").Equals(true))
        {
            _isAttack = true;
        }

        else
            _isAttack = false;
    }

    public void Attack()
    {
        if (_canAttack == true && _groundCheck.isGrounded)
        {

            _anim.SetTrigger("Attack1");

            StartCoroutine(AttackCooldown(1f));
            switch (LevelManager.instance.PlayerNumber)
            {
                case (int)LevelManager.Chars.MedievalWarrior:
                    SoundManager.instance.AttackSwordS();
                    break;
                case (int)LevelManager.Chars.MartialHero:
                    SoundManager.instance.AttackSwordS();

                    SoundManager.instance.AttackSwordS();
                    break;
                case (int)LevelManager.Chars.MartialHero2:
                    SoundManager.instance.AttackSwordS();

                    SoundManager.instance.AttackSwordS();
                    break;
                case (int)LevelManager.Chars.MartialHero3:
                    SoundManager.instance.AttackSwordS();
                    SoundManager.instance.AttackSwordS();
                    break;
                case (int)LevelManager.Chars.Hunterss:
                    SoundManager.instance.AttackSwordS();
                    break;
                case (int)LevelManager.Chars.Hunterss2:
                    break;
                case (int)LevelManager.Chars.Wizard:
                    break;
                default:
               
                    break;
            }

        }

            
        
    }
    public void Attack2()
    {
        if (_canAttack2 == true && _groundCheck.isGrounded && _isAttack == false)
        {
            switch (LevelManager.instance.PlayerNumber)
            {
                case (int)LevelManager.Chars.MedievalWarrior:
                  
                   
                    SoundManager.instance.AttackSwordS();
                    break;
                case (int)LevelManager.Chars.MartialHero:
                  
                   
                    SoundManager.instance.AttackSwordS();
                    break;
                case (int)LevelManager.Chars.MartialHero2:
                
                  
                    SoundManager.instance.AttackSwordS();
                    break;
                case (int)LevelManager.Chars.MartialHero3:
                 
                   
                    SoundManager.instance.AttackSwordS();
                    break;

                case (int)LevelManager.Chars.Hunterss:               
                 
                    break;
                case (int)LevelManager.Chars.Hunterss2:
                  
                    break;
                case (int)LevelManager.Chars.Wizard:
                 
                    break;


                default:
                 
                    break;
            }
            StartCoroutine(AttackCooldown2(5f));

            _anim.SetTrigger("Attack2");

        }
    }
        //public void Win()
        //{
        //    _anim.SetTrigger("Win");
        //    WinPanel.gameObject.SetActive(true);
        //    Canmove = false;
        //    _rb.velocity = new Vector2(0, _rb.velocity.y);
        //    _isGame = false;

        //}

        #region Player1
        public IEnumerator AttackCollider()
        {
            _c1.enabled = true;
            yield return new WaitForSeconds(.25f);
            _c1.enabled = false;
        }
        public IEnumerator AttackCollider2()
        {
            _c2.enabled = true;
            yield return new WaitForSeconds(.25f);
            _c2.enabled = false;
    }
    public IEnumerator JumpSoundCooldown()
    {
        _jumpsound = false;
        yield return new WaitForSeconds(.2f);
        _jumpsound = true;
    }

    IEnumerator AttackCooldown(float time)
        {

            _canAttack = false;
            yield return new WaitForSeconds(time);
            _canAttack = true;
        }
        IEnumerator AttackCooldown2(float time)
        {
            _canAttack2 = false;
            yield return new WaitForSeconds(.2f);
            _anim.ResetTrigger("Attack2");
            yield return new WaitForSeconds(time);
            _canAttack2 = true;
        }
        #endregion


        IEnumerator Died()
        {
            _rb.constraints = RigidbodyConstraints2D.FreezeAll;
            _rb.velocity = new Vector2(0, _rb.velocity.y);
            _canmove = false;
        yield return null;



            //_rb.constraints = RigidbodyConstraints2D.None;
            //_rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            //Physics2D.IgnoreLayerCollision(9, 10, false);
            //Canmove = true;

        }
   

}




