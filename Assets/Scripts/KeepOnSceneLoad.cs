using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class KeepOnSceneLoad : MonoBehaviour
{
    public int maxInstance = 1;
    public string tag;

    // Start is called before the first frame update

    private void Reset()
    {
        tag = this.gameObject.tag;
    }
    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(tag);

        if (objs.Length > maxInstance)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
