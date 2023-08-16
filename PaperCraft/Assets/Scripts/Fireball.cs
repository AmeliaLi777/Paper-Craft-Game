using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public Vector2 InitPos;
    public float InitSpeed;

    public Destination Destination;

    private float speed;
    private Vector2 direction;
    private float distance;
    private Vector2 position {
        get{
            return (Vector2)transform.position;
        }
        set{
            transform.position = (Vector3)value;
        }
    }

    private bool canHit = false;

    private GameControl gm => GameControl.Instance;

    public void Init(Vector2 pos, Vector2 dest){
        InitPos = pos;
        Destination.Init(dest);
        position = pos;
        direction = (dest - pos).normalized;
        speed = gm.InitSpeed;
    }

    public void Expire(){
        Destroy(transform.parent.gameObject);
    }

    public bool TryHit(){
        if (canHit){
            Destination.OnSuccess();
            Destroy(this.gameObject);
            return true;
        }
        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Move
        position += speed * Time.deltaTime * direction;
        speed -= speed * gm.Drag * Time.deltaTime;
        speed = Mathf.Max(speed, gm.MinSpeed);

        // Detect expire
        bool hit = (Destination.Position - position).sqrMagnitude < gm.HitThreshold * gm.HitThreshold;
        if (canHit && !hit){
            gm.ExpireFireball(this);
        }
        canHit = hit;
    }
}
