using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingPuzzle : MonoBehaviour
{
    public Texture2D image;
    public int blocksPerLine = 4;
    public int shuffleLength = 5;
    public float defaultMoveDuration = 0.3f;
    public float shuffleMoveDuration = 0.1f;

    enum PuzzleState {Start, Solved, Shuffling, Playing};
    PuzzleState state;

    BlockInput emptyBlock;
    BlockInput[,] blocks;
    Queue<BlockInput> inputs;
    bool blockIsMoving;
    int shuffleMovesRemaining;
    Vector2Int prevShuffleOffset;

    // Start is called before the first frame update
    void Start()
    {
        CreatePuzzle();

        if (state == PuzzleState.Start)
        {
            StartShuffle();
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartShuffle();
        }
    }

    void CreatePuzzle()
    {
        blocks = new BlockInput[blocksPerLine, blocksPerLine];
        Texture2D[,] imageSlices = ImageSlicer.GetSlices(image, blocksPerLine);
        for (int y = 0; y < blocksPerLine; y++)
        {
            for (int x = 0; x < blocksPerLine; x++)
            {
                GameObject blockObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
                blockObject.transform.position = -Vector2.one * (blocksPerLine - 1) * 0.5f + new Vector2(x, y);
                blockObject.transform.parent = transform;

                BlockInput block = blockObject.AddComponent<BlockInput>();
                block.OnBlockPressed += PlayerMoveBlockInput;
                block.OnFinishedMoving += OnBlockFinishedMoving;
                block.Init(new Vector2Int(x, y), imageSlices[x, y]);
                blocks[x, y] = block;

                if (y == 0 && x == blocksPerLine - 1)
                {
                    emptyBlock = block;
                }
            }

        }

        Camera.main.orthographicSize = blocksPerLine * 0.55f;
        inputs = new Queue<BlockInput>();

    }

    void PlayerMoveBlockInput(BlockInput blockToMove)
    {
        if (state == PuzzleState.Playing) 
        {
            inputs.Enqueue(blockToMove);
            MakeNextPlayerMove();
        }
        
    }

    void MakeNextPlayerMove()
    {
        while (inputs.Count > 0 && !blockIsMoving)
        {
            MoveBlock(inputs.Dequeue(), defaultMoveDuration);
        }
    }

    void MoveBlock(BlockInput blockToMove, float duration)
    {
        if ((blockToMove.coord - emptyBlock.coord).sqrMagnitude == 1)
        {
            blocks[blockToMove.coord.x, blockToMove.coord.y] = emptyBlock;
            blocks[emptyBlock.coord.x, emptyBlock.coord.y] = blockToMove;

            Vector2Int targetCoord = emptyBlock.coord;
            emptyBlock.coord = blockToMove.coord;
            blockToMove.coord = targetCoord;

            Vector2 targetPosition = emptyBlock.transform.position;
            emptyBlock.transform.position = blockToMove.transform.position;
            blockToMove.MoveToPosition(targetPosition, duration);
            blockIsMoving = true;
        }

    }

    void OnBlockFinishedMoving()
    {
        blockIsMoving = false;
        CheckIfSolved();

        if (state == PuzzleState.Playing) 
        { 
            MakeNextPlayerMove(); 
        }

        else if (state == PuzzleState.Shuffling)
        {
            if (shuffleMovesRemaining > 0)
            {
                MakeNextShuffleMove();
            }
            else
            {
                state = PuzzleState.Playing;
            }
        }
       
    }

    void StartShuffle()
    {
        state = PuzzleState.Shuffling;
        shuffleMovesRemaining = shuffleLength;
        emptyBlock.gameObject.SetActive(false);
        MakeNextShuffleMove();
    }


    void MakeNextShuffleMove()
    {
        Vector2Int[] offsets =
        {
            new Vector2Int(1, 0), new Vector2Int(-1, 0), new Vector2Int(0, 1), new Vector2Int(0, -1)
        };
        int randomIndex = Random.Range(0, offsets.Length);

        for (int i = 0; i < offsets.Length; i++)
        {
            Vector2Int offset = offsets[(randomIndex + i) % offsets.Length];
            if(offset != prevShuffleOffset * -1)
            {
                Vector2Int moveBlockCoord = emptyBlock.coord + offset;

                if (moveBlockCoord.x >= 0 && moveBlockCoord.x < blocksPerLine && moveBlockCoord.y >= 0 && moveBlockCoord.y < blocksPerLine)
                {
                    MoveBlock(blocks[moveBlockCoord.x, moveBlockCoord.y], shuffleMoveDuration);
                    prevShuffleOffset = offset;
                    shuffleMovesRemaining--;
                    break;
                }
            }
           
        }

    }


    IEnumerator FadeIn(bool fading)
    {
        if (fading)
        {
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                // set color with i as alpha
                emptyBlock.color = new Color(1, 1, 1, i);
                yield return null;
                emptyBlock.gameObject.SetActive(true);
            }
        }
    }

    void CheckIfSolved()
    {
        foreach (BlockInput block in blocks)
        {
            if (!block.IsAtStartingCoord())
            {
                return;
            }
        }

        state = PuzzleState.Solved;
        StartCoroutine(FadeIn(true));

    }


}
