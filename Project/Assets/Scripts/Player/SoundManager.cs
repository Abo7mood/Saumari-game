using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

  public static  SoundManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            if (instance != this)
            {
                Destroy(instance.gameObject);
                instance = this;
            }
        }

    }
    [SerializeField] AudioSource MainSource;
    [SerializeField] AudioSource SecoundSource;

    [SerializeField] AudioClip[] Audios;

    
    enum Audio {AttackSword,AttackMagic,AttackArrow,JumpPlayer,HitPlayer,DeadPlayer,HitEnemy,Explosion,_coins,walk }
    
       
    
   
    public void AttackSwordS() => SecoundSource.PlayOneShot(Audios[0], .4f);
    public void AttackMagicS() => SecoundSource.PlayOneShot(Audios[1], 1f);
    public void AttackArrowS() => SecoundSource.PlayOneShot(Audios[2], 1f);
    public void JumpPlayerS() => SecoundSource.PlayOneShot(Audios[3], 1f);
    public void HitPlayer() => SecoundSource.PlayOneShot(Audios[4], 1f);
    public void DeadPlayerS() => SecoundSource.PlayOneShot(Audios[5], 1f);
    public void HitEnemy() => SecoundSource.PlayOneShot(Audios[6], 1f);
    public void Explosion() => SecoundSource.PlayOneShot(Audios[7], 1f);
    public void _coins() => SecoundSource.PlayOneShot(Audios[8], 1f);
    public void Walk() 
    { 
        if(!SecoundSource.isPlaying)
            SecoundSource.PlayOneShot(Audios[9], 1f);
    } 


}
