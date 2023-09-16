using UnityEngine;
using UnityEngine.U2D;

public class MatchingTimingStyleGame : TimingStyleGame
{
    [SerializeField] private Transform[] blocks;
    [SerializeField] private Transform line;
    [SerializeField] private SpriteAtlas atlas;

    [SerializeField] private float lineMin;
    [SerializeField] private float lineMax;
    [SerializeField] private float speedMin;
    [SerializeField] private float speedMax;
    
    [SerializeField] private float cutline;
    
    [SerializeField] private TimeBar timeBar;

    private float baseLineY;
    private float[] blockSpeeds;
    private int blockIndex;
    protected override void StartGame()
    {
        base.StartGame();

        baseLineY = Random.Range(lineMin, lineMax);
        line.localPosition = new Vector3(0, baseLineY, 0);
        blockSpeeds = new float[blocks.Length];
        for (int i = 0; i < blocks.Length; i++)
        {
            var pos = blocks[i].localPosition;
            pos.y = Random.Range(lineMin, lineMax);
            blocks[i].localPosition = pos;
            blockSpeeds[i] = Random.Range(speedMin, speedMax);

            var sprite = blocks[i].GetComponent<SpriteRenderer>();
            sprite.sprite = atlas.GetSprite($"{Random.Range(0, 61):D2}");
        }
    }

    protected override bool UpdateGame()
    {
        timeBar.SetFillAmount(1 - gameTime / limitTime);
        
        if (IsPlaying)
        {
            for (int i = blockIndex; i < blocks.Length; i++)
            {
                var pos = blocks[i].localPosition;
                pos.y += blockSpeeds[i] * Time.deltaTime;
                if (pos.y > lineMax)
                {
                    pos.y = lineMax;
                    blockSpeeds[i] *= -1;
                }
                else if (pos.y < lineMin)
                {
                    pos.y = lineMin;
                    blockSpeeds[i] *= -1;
                }

                blocks[i].localPosition = pos;
            }
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StopAndCheckTiming();
            }
        }
        
        return base.UpdateGame();
    }
    
    protected override void StopAndCheckTiming()
    {
        if (!IsSuccess(blockIndex))
        {
            blocks[blockIndex].GetComponent<SpriteRenderer>().color = Color.gray;
        }
        
        blockIndex++;
        
        if (blockIndex >= blocks.Length)
        {
            FinishGame(IsAllSuccess());
        }
    }

    private bool IsSuccess(int index)
    {
        return Mathf.Abs(blocks[index].localPosition.y - baseLineY) <= cutline;
    }

    private bool IsAllSuccess()
    {
        int failCount = 0;
        for (int i = 0; i < blockIndex; i++)
        {
            if (!IsSuccess(i))
            {
                failCount++;
            }
        }

        return failCount < 3;
    }
}