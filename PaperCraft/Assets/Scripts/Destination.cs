using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{
    public Vector2 Position;

    private GameControl gm => GameControl.Instance;

    public void Init(Vector2 position){
        Position = position;
        transform.position = position;
    }

    public void OnSuccess(){
        Debug.Log("taaaadaaaaa");
        Destroy(transform.parent.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
