using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // Configuration Parameters
    [SerializeField] AudioClip[] explosions;
    Level level;
    GameStatus gameStatus;

    private void Start()
    {
        level = FindObjectOfType<Level>();
        level.CountBreakableBlocks();
        gameStatus = FindObjectOfType<GameStatus>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DestroyBlock(collision);
    }

    private void DestroyBlock(Collision2D collision)
    {
        if (collision.gameObject.name == "Ball")
        {
            gameStatus.AddToScore();
            AudioClip clip = explosions[UnityEngine.Random.Range(0, explosions.Length)];
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
            Destroy(gameObject);
            level.BlockDestroyed();
        }
    }
}
