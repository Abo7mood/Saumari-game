using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EnemyController : MonoBehaviour
{
	#region int&float
	int __coins;
	[SerializeField] float _length;
	[SerializeField] float _speed;
	float _waitTime, _startWaitTime;
	float _distanceToAttackPlayer;

	[SerializeField] int _minX = -88;
	[SerializeField] int _maxX = -50;
	[SerializeField] int _damage;
	int _MaxHealth = 100;
	[SerializeField] int _attackSpeed = 8;
	[SerializeField] int _patrolSpeed = 4;

	#endregion

	#region boolean
	public bool _movingright = true;
	private bool _isDamaged = false;
	bool _attacking=true;
	[HideInInspector] public bool _isDead;
	bool _canMove=true;
	#endregion

	#region strings
	string medival1 = "C1"; string medival2 = "C2"; string martialheroC1 = "C3"; string martialheroC2 = "C4";
	string martialhero2C1 = "C5"; string martialhero2C2 = "C6"; string martialhero3C1 = "C7"; string martialhero3C2 = "C8";
	string HunterC1 = "C9"; string WizardC1 = "C10"; string WizardC2 = "C11";


	const string Enemy1 = "Enemy(Clone)";
	const string EnemySword = "EnemySword(Clone)";
	const string EnemySword1 = "EnemySword 1(Clone)";
	const string EnemyGoblin = "Goblin(Clone)";
	const string EnemyGoblinBoom = "GoblinBoom(Clone)";
	const string FireWorm = "FireWorm(Clone)";
	const string FireWorm1 = "FireWorm 1(Clone)";






	#endregion

	#region Construst
	[Header("Construct")]
	[SerializeField] Collider2D _e1;
	BoxCollider2D _box;
	private Collider2D _playerCollider;
	[SerializeField] Transform _groundDetection;
	[SerializeField] Animator _anim;
	Transform _player;
	PlayerController _playerscript;
	[SerializeField] Vector2 _relativePoint;
	[SerializeField] SpriteRenderer _enemySprite;
	[SerializeField] GameObject _canvasTransform;
	[SerializeField] HealthBarFade1 _fade1;
	[SerializeField] GameObject _sword;

	[SerializeField] Transform _swordDirection;
	[SerializeField] GameObject Diamond;
	private GameObject _realSword;
	Rigidbody2D _rb;
	
	Vector2 _startColliderSize;
	[SerializeField] float Rotation;
	[Header("GoblinBoom")]
	[SerializeField] GameObject BoomPrefab;
	[SerializeField] GameObject DamageText;

	PlayerInfo _playerInfo;
	#endregion

	private void Awake()
	{
		_playerInfo = FindObjectOfType<PlayerInfo>().GetComponent<PlayerInfo>();
		_player = FindObjectOfType<PlayerController>().transform;
		_box = GetComponent<BoxCollider2D>();
			_rb = GetComponent<Rigidbody2D>();
		_playerscript = FindObjectOfType<PlayerController>().GetComponent<PlayerController>();
		_playerCollider = FindObjectOfType<PlayerController>().gameObject.GetComponent<Collider2D>();
	}
	// Start is called before the first frame update
	void Start()
	{
        switch (gameObject.name)
        {
			case "Enemy(Clone)":
				__coins = 5;

				break;
			case "GoblinBoom(Clone)":
				__coins = 10;
				break;
			case "Goblin(Clone)":
				__coins = 15;
				break;
			case "FireWorm(Clone)":
			case "FireWorm 1(Clone)":
				__coins = 25;
				gameObject.transform.rotation = new Quaternion(0, Rotation, 0, 0);
				break;
			case "EnemySword(Clone)":
			case "EnemySword 1(Clone)":
				__coins = 8;
				gameObject.transform.rotation = new Quaternion(0,Rotation,0,0);
				break;


			default:
				

				break;
        }

        _waitTime = _startWaitTime;
		if (gameObject.name == Enemy1)
			return;
		else
			InvokeRepeating(nameof(AttackSword), 0, 3f);
		//randomSpot.position = new Vector2(Random.Range(minX, maxX), 2.13f);
	}

	
	void Update() 
	{
		
		DetectPlayer();
		_canvasTransform.transform.localPosition = new Vector2(transform.position.x + -0.05499995f, transform.position.y + 0.5f);
		if (_canMove)
        {
			if(gameObject.name == Enemy1|| gameObject.name == EnemyGoblin)
				Patrol1();
		}
		
		
	} 
   
    void Patrol1()
	{
		
		transform.Translate(Vector2.right * _speed * Time.deltaTime);

		RaycastHit2D groundinfo = Physics2D.Raycast(_groundDetection.position, Vector2.down, _length);
		if (groundinfo.collider == false)
		{
			if (_movingright == true)
			{
				transform.eulerAngles = new Vector3(0, -180, 0);
				_movingright = false;
			}
			else
			{
				transform.eulerAngles = new Vector3(0, 0, 0);
				_movingright = true;
			}
		}

	}

	void DetectPlayer()
	{
		_relativePoint = transform.InverseTransformPoint(_playerscript.transform.position);
		if (_relativePoint.x < 0f && Mathf.Abs(_relativePoint.x) > Mathf.Abs(_relativePoint.y))
			Debug.Log("right");
		if (_relativePoint.x > 0f && Mathf.Abs(_relativePoint.x) > Mathf.Abs(_relativePoint.y))
			Debug.Log("left");
		if (_relativePoint.y > 0 && Mathf.Abs(_relativePoint.x) < Mathf.Abs(_relativePoint.y))
			Debug.Log("right");
		if (_relativePoint.y < 0 && Mathf.Abs(_relativePoint.x) < Mathf.Abs(_relativePoint.y))
			Debug.Log("down");
	}
	void AttackThePlayer()
	{

		if (Vector2.Distance(transform.position, _playerscript.transform.position) < Mathf.Abs(_distanceToAttackPlayer))
		{
			_playerscript.Dead();
			//LevelManager.Instance.PlayerLose();

		}

		else
			_attacking = false;
	}
	public void Attack()
    {
        if (_attacking==true)
        {
			_anim.SetTrigger("Attack");
			StartCoroutine(nameof(AttackEnum));
		}
		   
    }
	public void AttackSword()=> _anim.SetTrigger("Attack");


	public void Dead()
	{
		_isDead = true;
		SoundManager.instance._coins();
        switch (gameObject.name)
        {
			case Enemy1:
				
				_playerInfo._coins += 5;
				SaveData.instance.Save();
				break;
			case EnemySword:
			case EnemySword1:
				_playerInfo._coins += 8;
				SaveData.instance.Save();
				break;
			case EnemyGoblin:
			
				_playerInfo._coins += 11;
				SaveData.instance.Save();
				break;
			case EnemyGoblinBoom:
				
				_playerInfo._coins += 13;
				SaveData.instance.Save();
				break;
			case FireWorm:
			case FireWorm1:
				_playerInfo._coins += 20;
				SaveData.instance.Save();
				break;

			default:
			
				break;
        }
        _canMove = false;
		_anim.SetTrigger("Dead");
		Destroy(this.gameObject, 1f);
		_box.enabled = false;
		_rb.constraints = RigidbodyConstraints2D.FreezeAll;

		GameObject RealDiamond = Instantiate(Diamond, _anim.transform.position, Quaternion.identity,null);
		GameObject TextObject = Instantiate(DamageText, _anim.transform.position, Quaternion.identity, null);
		TextObject.GetComponent<TextMeshPro>().text = __coins.ToString();
		Destroy(RealDiamond, 2f);
		Destroy(TextObject, 2f);

	}
	private void OnTriggerStay2D(Collider2D c)
	{
		if (c.name == medival1 && _isDamaged == false&&_isDead==false)
		{
			SoundManager.instance.HitEnemy();
			Shaker.Instance.CamShaker(1f, 2f, .1f, .1f);
			StartCoroutine(IsDamaged());
			_anim.SetTrigger("Hit");
			_fade1.Damage(10);
			


		}
		else if (c.name == medival2 && _isDamaged == false&&_isDead==false)
		{
			SoundManager.instance.HitEnemy();
			Shaker.Instance.CamShaker(1.5f, 3f, .1f, .1f);
			StartCoroutine(IsDamaged());
			_anim.SetTrigger("Hit");
			_fade1.Damage(20);
			
		}
		else if (c.name == martialheroC1 && _isDamaged == false&&_isDead==false)
		{
			SoundManager.instance.HitEnemy();
			Shaker.Instance.CamShaker(1.5f, 3f, .1f, .1f);
			StartCoroutine(IsDamaged());
			_anim.SetTrigger("Hit");
			_fade1.Damage(30);	
		}
		else if (c.name == martialheroC2 && _isDamaged == false&&_isDead==false)
		{
			SoundManager.instance.HitEnemy();
			Shaker.Instance.CamShaker(1.5f, 3f, .1f, .1f);
			StartCoroutine(IsDamaged());
			_anim.SetTrigger("Hit");
			_fade1.Damage(60);
			
		}
		else if (c.name == martialhero2C1 && _isDamaged == false&&_isDead==false)
		{
			SoundManager.instance.HitEnemy();
			Shaker.Instance.CamShaker(1.5f, 3f, .1f, .1f);
			StartCoroutine(IsDamaged());
			_anim.SetTrigger("Hit");
			_fade1.Damage(12);
		
		}
		else if (c.name == martialhero2C2 && _isDamaged == false&&_isDead==false)
		{
			SoundManager.instance.HitEnemy();
			Shaker.Instance.CamShaker(1.5f, 3f, .1f, .1f);
			StartCoroutine(IsDamaged());
			_anim.SetTrigger("Hit");
			_fade1.Damage(24);
			
		}
		else if (c.name == martialhero3C1 && _isDamaged == false&&_isDead==false)
		{
			SoundManager.instance.HitEnemy();
			Shaker.Instance.CamShaker(1.5f, 3f, .1f, .1f);
			StartCoroutine(IsDamaged());
			_anim.SetTrigger("Hit");
			_fade1.Damage(27);
			
		}
		else if (c.name == martialhero3C2 && _isDamaged == false&&_isDead==false)
		{
			SoundManager.instance.HitEnemy();
			Shaker.Instance.CamShaker(1.5f, 3f, .1f, .1f);
			StartCoroutine(IsDamaged());
			_anim.SetTrigger("Hit");
			_fade1.Damage(52);
			
		}
        else if (c.name == HunterC1 && _isDamaged == false&&_isDead==false)
        {
			SoundManager.instance.HitEnemy();
			Shaker.Instance.CamShaker(1.5f, 3f, .1f, .1f);
            StartCoroutine(IsDamaged());
            _anim.SetTrigger("Hit");
            _fade1.Damage(36);
        }
		else if (c.name == WizardC1 && _isDamaged == false&&_isDead==false)
		{
			SoundManager.instance.HitEnemy();
			Shaker.Instance.CamShaker(4f, 5f, .5f, .5f);
			StartCoroutine(IsDamaged());
			_anim.SetTrigger("Hit");
			_fade1.Damage(35);


		}
		else if (c.name == WizardC2 && _isDamaged == false&&_isDead==false)
		{
			SoundManager.instance.HitEnemy();
			Shaker.Instance.CamShaker(5f, 6f, .7f, .7f);
			StartCoroutine(IsDamaged());
			_anim.SetTrigger("Hit");
			_fade1.Damage(75);


		}
		else if (c.name == "Arrow" && _isDamaged == false&&_isDead==false)
		{
			SoundManager.instance.HitEnemy();
			Destroy(c.gameObject,0);
			Shaker.Instance.CamShaker(1.5f, 3f, .1f, .1f);
			StartCoroutine(IsDamaged());
			_anim.SetTrigger("Hit");
			_fade1.Damage(24);
		

		}
		else if (c.name == "ArrowP" && _isDamaged == false&&_isDead==false)
		{
			SoundManager.instance.HitEnemy();
			Destroy(c.gameObject, 0);
			Shaker.Instance.CamShaker(3.5f, 4f, .1f, .1f);
			StartCoroutine(IsDamaged());
			_anim.SetTrigger("Hit");
			_fade1.Damage(50);


		}
		else if (c.name == "Spear" && _isDamaged == false&&_isDead==false)
		{
			SoundManager.instance.HitEnemy();
			Destroy(c.gameObject, 0);
			Shaker.Instance.CamShaker(4f, 5f, .3f, .3f);
			StartCoroutine(IsDamaged());
			_anim.SetTrigger("Hit");
			_fade1.Damage(60);


		}
		
		else
			return;
	}
	
	public void Sword()
	{
		_realSword = Instantiate(_sword, _swordDirection.position,Quaternion.identity);
		if(gameObject.name== "EnemySword 1(Clone)"|| gameObject.name == "FireWorm 1(Clone)")
        {
			_realSword.GetComponent<EnemySword>()._speed = 5;
        }else
			_realSword.GetComponent<EnemySword>()._speed = -5;
		Destroy(_realSword, 1);

	}
	
	public void Bomb()
    {
	GameObject RealBomb=	Instantiate(BoomPrefab, transform.position, Quaternion.identity, null);
    }
	private IEnumerator AttackEnum()
    {
		_attacking = false;
		yield return new WaitForSeconds(.5f);
		_anim.ResetTrigger("Attack");
		
		yield return new WaitForSeconds(3f);
		_attacking = true;
	}
	private IEnumerator IsDamaged()
	{
		_isDamaged = true;
		yield return new WaitForSeconds(.5f);
		_isDamaged = false;
	}
	public IEnumerator AttackCollider()
	{
		_e1.enabled = true;
		yield return new WaitForSeconds(.1f);
		_e1.enabled = false;
	}


}


