using System.Collections.Generic;
using UnityEngine;

public class FluidTimingStyleGame : TimingStyleGame
{
    [SerializeField] private GameObject particle;
    [SerializeField] private Transform lineMin;
    [SerializeField] private Transform lineMax;
    
    [SerializeField] private float lineMinY;
    [SerializeField] private float lineMaxY;
    
    [SerializeField] private int minPercent;
    [SerializeField] private int maxPercent;
    [SerializeField] private int particleMax;
    
    private float deltaTime;
    private float speed = 1f;
    private List<GameObject> particles = new List<GameObject>();
    
    private bool pressSpace;

    protected override void StartGame()
    {
        base.StartGame();

        var posMin = lineMin.localPosition;
        posMin.y = minPercent * 0.01f * (lineMaxY - lineMinY) + lineMinY;
        lineMin.localPosition = posMin;
        
        var posMax = lineMax.localPosition;
        posMax.y = maxPercent * 0.01f * (lineMaxY - lineMinY) + lineMinY;
        lineMax.localPosition = posMax;
    }

    protected override int GetResult()
    {
        return particles.Count;
    }

    protected override bool UpdateGame()
    {
        if (IsPlaying && !pressSpace)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                deltaTime += Time.deltaTime * speed;
                if (deltaTime > 0.01f)
                {
                    SpawnParticle();
                    SpawnParticle();
                    deltaTime -= 0.01f;
                }
                
                speed += Random.Range(0, 5) * Time.deltaTime;
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                pressSpace = true;
                Invoke("StopAndCheckTiming", 2.5f);
            }
        }

        return base.UpdateGame();
    }

    private void SpawnParticle()
    {
        var pos = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0);
        var obj = Instantiate(particle, particle.transform.parent);
        obj.transform.SetLocalPositionAndRotation(pos, Quaternion.identity);
        obj.SetActive(true);
        
        particles.Add(obj);
    }
    
    protected override void StopAndCheckTiming()
    {
        FinishGame(IsSuccess());
    }

    private bool IsSuccess()
    {
        Debug.Log($"{particleMax} * {minPercent} * 0.01f <= {particles.Count} && {particles.Count} <= {particleMax} * {maxPercent} * 0.01f");
        var result = particleMax * minPercent * 0.01f <= particles.Count && particles.Count <= particleMax * maxPercent * 0.01f;
        Debug.Log(result);
        return result;
    }
}