using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    [SerializeField] EnemyController _enemy;
    [SerializeField] PlayerController _player;
    [SerializeField] Arrow _arrow;
   
    public void EnableColliderE1() => StartCoroutine(_enemy.AttackCollider());
    public void EnableColliderC1() => StartCoroutine(_player.AttackCollider());
    public void EnableColliderC2() => StartCoroutine(_player.AttackCollider2());
    public void ArrowCreate() => _arrow.ArrowInstantiate( );

    public void ArrowCreateP() => _arrow.ArrowPInstantiate();

    public void SpearCreate() => _arrow.SpearInstantiate();

    public void SwordInstantiate() => _enemy.Sword();

    public void BombInstantiate() => _enemy.Bomb();
 
    public void MagicAttack() => SoundManager.instance.AttackMagicS();
    public void ArrowAttack() => SoundManager.instance.AttackArrowS();
    public void ExplosionSound() => SoundManager.instance.Explosion();



}
