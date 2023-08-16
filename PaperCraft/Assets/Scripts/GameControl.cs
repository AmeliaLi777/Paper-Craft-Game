using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public static GameControl Instance;
    private void Awake() {
        Instance = this;
    }

    public int score;

    public List<Fireball> ActiveFireballs;

    public Rect Range;

    public float InitSpeed;
    public float MinSpeed;
    public float Drag;

    public float HitThreshold;

    public Camera MainCamera;
    public GameObject FireballPrefab;
    public List<Firework> FireworkPrefabs;

    public void CreateFireball(Vector2 pos, Vector2 dest){
        Fireball fb = Instantiate(FireballPrefab).GetComponentInChildren<Fireball>();
        ActiveFireballs.Add(fb);
        fb.Init(pos, dest);
    }

    public void ExpireFireball(Fireball fb){
        score = Mathf.Max(score - 1, 0);
        ActiveFireballs.Remove(fb);
        fb.Expire();
    }

    public void CreateFirework(Vector2 pos, float life = 5f){
        score = Mathf.Min(score + 1, 35);
        Firework fw = Instantiate(FireworkPrefabs[UnityEngine.Random.Range(0, FireworkPrefabs.Count)]);
        fw.Init(pos, life);
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        Vector2 screenMin = MainCamera.ScreenToWorldPoint(Vector3.zero);
        Range = new Rect(screenMin, -2 * screenMin);

        while (true){
            yield return new WaitForSeconds(CalcInterval(score));
            CreateFireball(GetStartPosition(), GetEndPosition());
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

    //private List<float> intervals = new List<float>()
    //    {3f, 3f, 3f, 2.9f, 2.8f, 2.6f, 2.4f, 2.2f, 2.0f, 1.8f, 1.6f, 1.4f, 1.2f, 1.1f, 1f};
    private float CalcInterval(int score){
        if (score < 5) return 3f;
        else if (score < 15) return Mathf.Lerp(3f, 1f, (score - 5) / 10f);
        else if (score < 25) return Mathf.Lerp(1f, 0.5f, (score - 15) / 10f);
        else return 0.5f;
    }

    private Vector2 GetStartPosition(){
        return new Vector2(UnityEngine.Random.Range(Range.xMin, Range.xMax), Range.yMin);
    }

    private Vector2 GetEndPosition(){
        return new Vector2(
            UnityEngine.Random.Range(Range.xMin + 1f, Range.xMax - 1f), 
            UnityEngine.Random.Range(1f, Range.yMax - 1f)
        );
    }
}
