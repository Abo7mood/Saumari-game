using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
public class Shaker : MonoBehaviour
{
    public static Shaker Instance;
    //[SerializeField]float  test1;
    //[SerializeField] float test2;
    //[SerializeField] float test3;
    //[SerializeField] float test4;
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(Instance.gameObject);

        Instance = this;

    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.N))
    //        CamShaker(test1, test2, test3, test4);
    //}
    public void CamShaker(float magnitude,float roughness,float fadein,float fadeout ) => CameraShaker.Instance.ShakeOnce(magnitude,roughness,fadein,fadeout);

}
