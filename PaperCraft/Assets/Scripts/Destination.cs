using System;
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
        Debug.Log("Hit!");
        gm.score = Mathf.Min(gm.score + 1, 35);
        gm.CreateFirework(Position, 5f);
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
