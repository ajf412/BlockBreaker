using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // Configuration Parameters
    [SerializeField] AudioClip[] explosions;
    [SerializeField] GameObject blockDestroyVFX;

    Level level;
    GameSession gameStatus;

    private void Start()
    {
        level = FindObjectOfType<Level>();
        level.CountBreakableBlocks();
        gameStatus = FindObjectOfType<GameSession>();
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
            PlayBlockDestroySFX();
            TriggerVFX();
            Destroy(gameObject);
            level.BlockDestroyed();
        }
    }

    private void PlayBlockDestroySFX()
    {
        AudioClip clip = explosions[UnityEngine.Random.Range(0, explosions.Length)];
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
    }

    private void TriggerVFX()
    {
        GameObject shrapnel = Instantiate(blockDestroyVFX, transform.position, transform.rotation);
        Destroy(shrapnel, 1);
    }
}
