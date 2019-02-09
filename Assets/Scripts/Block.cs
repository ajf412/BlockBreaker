using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // Configuration Parameters
    [SerializeField] AudioClip[] explosions;
    [SerializeField] GameObject blockDestroyVFX;
    // [SerializeField] int maxHits;
    [SerializeField] Sprite[] hitSprites;

    // Cached References
    Level level;
    GameSession gameStatus;

    // State Variables
    [SerializeField] int timesHit;  //  TODO only here for debugging.  Delete later.

    private void Start()
    {
        CountBreakableBlocks();
        gameStatus = FindObjectOfType<GameSession>();
    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();
        if (tag == "Breakable")
        {
            level.CountBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ball" && tag == "Breakable")
        {
            HandleHit();
        }
    }

    private void HandleHit()
    {
        timesHit++;
        int maxHits = hitSprites.Length + 1;
        if (timesHit >= maxHits)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit - 1;
        if(hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.LogError(gameObject.name + ": Block sprite is missing from array.");
        }
    }

    private void DestroyBlock()
    {
        gameStatus.AddToScore();
        PlayBlockDestroySFX();
        TriggerVFX();
        Destroy(gameObject);
        level.BlockDestroyed();
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
