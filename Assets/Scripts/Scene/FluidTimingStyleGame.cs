using UnityEngine;

public class FluidTimingStyleGame : TimingStyleGame
{
    [SerializeField] private GameObject particle;
    private float deltaTime;
    protected override void StartGame()
    {
        
    }

    protected override void FinishGame()
    {
        throw new System.NotImplementedException();
    }

    protected override int GetResult()
    {
        throw new System.NotImplementedException();
    }

    protected override bool UpdateGame()
    {
        deltaTime += Time.deltaTime;
        if (deltaTime > 0.01f)
        {
            SpawnParticle();
            SpawnParticle();
            deltaTime -= 0.01f;
        }
        
        return base.UpdateGame();
    }

    private void SpawnParticle()
    {
        var pos = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f),
            Random.Range(-0.1f, 0.1f));
        var obj = Instantiate(particle, particle.transform.parent);
        obj.transform.SetLocalPositionAndRotation(pos, Quaternion.identity);
        obj.SetActive(true);
    }
}