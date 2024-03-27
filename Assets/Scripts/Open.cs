using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Open : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase tile;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector3Int cellPosition = tilemap.WorldToCell(collision.transform.position);
            Debug.Log(cellPosition);
            tilemap.SetTile(new Vector3Int(-6, -3, 0), tile);
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }

    private void Toggle()
    {
        
    }
}
