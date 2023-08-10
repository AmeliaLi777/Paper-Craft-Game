using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public static GameControl Instance;
    private void Awake() {
        Instance = this;
    }

    public List<Fireball> ActiveFireballs;

    public Rect Range;

    public float InitSpeed;
    public float MinSpeed;
    public float Acceleration;

    public float HitThreshold;

    public GameObject FireworkPrefab;

    public void CreateFireball(Vector2 pos, Vector2 dest){
        Fireball fb = Instantiate(FireworkPrefab).GetComponentInChildren<Fireball>();
        ActiveFireballs.Add(fb);
        fb.Init(pos, dest);
    }

    public void ExpireFireball(Fireball fb){
        ActiveFireballs.Remove(fb);
        fb.Expire();
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (true){
            yield return new WaitForSeconds(5f);
            CreateFireball(new Vector2(-20f, -10f), new Vector2(7f, 3f));
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.A)){
            for (int i = ActiveFireballs.Count - 1; i >= 0; i--)
            {
                if (ActiveFireballs[i].TryHit()){
                    ActiveFireballs.RemoveAt(i);
                }
            }
        }
    }
}
