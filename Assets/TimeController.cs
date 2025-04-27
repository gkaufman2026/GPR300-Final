using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class TimeController : MonoBehaviour
{
    [SerializeField] public GameObject DirectionalLight;
    public float time;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time = Time.deltaTime;
        DirectionalLight.transform.Rotate(new Vector3(Mathf.Lerp(0, 360, time / 10f), 0,0));
        Debug.Log(Mathf.Lerp(0, 360, time / 10f));
        if (time > 10)
        {
            time = 0;
        }
    }
}
