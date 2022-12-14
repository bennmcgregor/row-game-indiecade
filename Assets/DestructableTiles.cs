using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class DestructableTiles : MonoBehaviour
{
    private Tilemap destructableTilemap;
    private TilemapCollider2D destructableTilemapCollider;
    [SerializeField] Toggle isEnabledToggle;
     
    // Start is called before the first frame update
    void Start()
    {
        destructableTilemap = GetComponent<Tilemap>();
        destructableTilemapCollider = GetComponent<TilemapCollider2D>();
        isEnabledToggle.onValueChanged.AddListener(delegate { ToggleTrigger(); });
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isEnabledToggle.isOn)
        {
            Vector3 collisionPoint = destructableTilemap.gameObject.GetComponent<Collider2D>().ClosestPoint(collision.gameObject.transform.position);
            collisionPoint.x = collisionPoint.x;
            collisionPoint.y = collisionPoint.y;

            for (int i = 0; i < ((CapsuleCollider2D)collision).size.x / 2; i++)
            {
                for (int j = 0; j < ((CapsuleCollider2D)collision).size.y / 2; j++)
                {
                    Vector3 tempPoint = Vector3.zero;
                    tempPoint.x = collisionPoint.x - (float)i;
                    tempPoint.y = collisionPoint.y - (float)j;

                    if (((CapsuleCollider2D)collision).OverlapPoint(new Vector2(tempPoint.x, tempPoint.y)))
                        destructableTilemap.SetTile(destructableTilemap.WorldToCell(tempPoint), null);

                    tempPoint.x = collisionPoint.x + (float)i;
                    tempPoint.y = collisionPoint.y + (float)j;

                    if (((CapsuleCollider2D)collision).OverlapPoint(new Vector2(tempPoint.x, tempPoint.y)))
                        destructableTilemap.SetTile(destructableTilemap.WorldToCell(tempPoint), null);

                    tempPoint.x = collisionPoint.x + (float)i;
                    tempPoint.y = collisionPoint.y - (float)j;

                    if (((CapsuleCollider2D)collision).OverlapPoint(new Vector2(tempPoint.x, tempPoint.y)))
                        destructableTilemap.SetTile(destructableTilemap.WorldToCell(tempPoint), null);

                    tempPoint.x = collisionPoint.x - (float)i;
                    tempPoint.y = collisionPoint.y + (float)j;

                    if (((CapsuleCollider2D)collision).OverlapPoint(new Vector2(tempPoint.x, tempPoint.y)))
                        destructableTilemap.SetTile(destructableTilemap.WorldToCell(tempPoint), null);
                }
            }
        }
    }

    private void ToggleTrigger()
    {
        destructableTilemapCollider.isTrigger = isEnabledToggle.isOn;
    }
}
