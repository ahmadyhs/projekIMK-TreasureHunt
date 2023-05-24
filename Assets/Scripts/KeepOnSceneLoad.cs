using UnityEngine;

public class KeepOnSceneLoad : MonoBehaviour
{
    public int maxInstance = 1;
    public string tagName;

    // Start is called before the first frame update

    private void Reset()
    {
        tagName = this.gameObject.tag;
    }
    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(tagName);

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
