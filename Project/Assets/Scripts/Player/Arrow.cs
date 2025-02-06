using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] GameObject _arrow;
    [SerializeField] GameObject _arrowP;
    [SerializeField] Transform _arrowdirection;
    private GameObject _realArrow;
    private GameObject _realArrowP;
    [Header("Spear")]
    private GameObject _realSpear;
    [SerializeField] GameObject Spear;
    [SerializeField] Transform _speardirection;
    [SerializeField] Transform _player;
  
   

    public void ArrowInstantiate()
    {
        SoundManager.instance.AttackArrowS();
        _realArrow = Instantiate(_arrow, _arrowdirection.position, _player.rotation);
      
       Destroy(_realArrow, 15);
        
    }
    public void ArrowPInstantiate()
    {    SoundManager.instance.AttackArrowS();
        _realArrowP = Instantiate(_arrowP, _arrowdirection.position, _player.rotation);

        Destroy(_realArrowP, 15);

    }
    public void SpearInstantiate()
    {
        SoundManager.instance.AttackArrowS();
        _realSpear = Instantiate(Spear, _speardirection.position, _player.rotation);

        Destroy(_realSpear, 15);

    }
   



}
