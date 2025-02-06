using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    private Animator _anim;
    [SerializeField] GameObject _enemy;
    EnemyController _enemyController;
    private void Awake()
    {
        _anim = _enemy.GetComponentInChildren<Animator>();
        _enemyController = _enemy.GetComponent<EnemyController>();
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            _enemyController.Attack();
    }
}
