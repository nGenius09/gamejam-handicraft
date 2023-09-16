using UnityEngine;

public class FluidTimingStyleGame : TimingStyleGame
{
    [SerializeField] private GameObject particle;
    private float deltaTime;
    private float speed = 1f;
    
    protected override void StartGame()
    {
        base.StartGame();
    }

    protected override void FinishGame()
    {
        base.FinishGame();
    }

    protected override int GetResult()
    {
        throw new System.NotImplementedException();
    }

    protected override bool UpdateGame()
    {
        if (isPlaying)
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
                StopAndCheckTiming();
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
    }
    
    protected override void StopAndCheckTiming()
    {
        isPlaying = false;
    }
}