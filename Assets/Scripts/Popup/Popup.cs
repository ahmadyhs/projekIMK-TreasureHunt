using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour
{
    
    public float t = 0f;
    private bool Tstarted = false;
    public float duration = 1f;
    //protected bool isFollowing = false;
    //protected Transform followTarget;
    // Start is called before the first frame update
    protected void Start()
    {
        //Debug.Log(transform.localScale);
    }

    // Update is called once per frame
    protected void Update()
    {
        if(Tstarted) t += Time.deltaTime;
        //if(t > duration) gameObject.SetActive(false);
        if(t > duration) GameObject.Destroy(gameObject);
        //if (isFollowing)
        //{
        //    transform.position = followTarget.position;
        //}
    }
    public void startT()
    {
        Tstarted= true;
    }
    public void stopT()
    {
        Tstarted= false;
    }

    //public void follow(Transform target)
    //{
    //    followTarget = target;
    //    isFollowing = true;
    //}
    //public void stopFollow()
    //{
    //    isFollowing = false;
    //}
}
