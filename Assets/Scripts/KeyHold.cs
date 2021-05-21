// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
//
// public class KeyHold : MonoBehaviour
// {
//     public bool hold;
//
//     public float distance = 2f;
//
//     private RaycastHit2D hit;
//
//     public Transform holdPoint;
//     public float throwObject = 5;
//
//     private Animator animator;
//
//     private Vector2Int exitPos;
//     
//     // Start is called before the first frame update
//     void Start()
//     {
//         animator = GetComponent<Animator>();
//         exitPos = RandomWalkGenerator.exitPos;
//     }
//
//     // Update is called once per frame
//     void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.F))
//         {
//             if (!hold)
//             { 
//                 Physics2D.queriesStartInColliders = false;
//                 hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance);
//                 if (!(hit.collider is null) && hit.collider.CompareTag("Key"))
//                     hold = true;
//             }
//
//             else
//             {
//                 // var curPos = (Vector2) transform.position;
//                 // if ((curPos.x >= exitPos.x - distance || curPos.x <= exitPos.x + distance) && (curPos.y >= exitPos.y - distance || curPos.y <= exitPos.y + distance))
//                 //     animator.SetBool("Open", true);
//                 hold = false;
//                 if (!(hit.collider.gameObject.GetComponent<Rigidbody2D>() is null))
//                 {
//                     var posAfterThrow = new Vector2(transform.position.x + 1, transform.position.y);
//                     hit.collider.gameObject.GetComponent<Rigidbody2D>().position = posAfterThrow;
//                     // if (posAfterThrow == exitPos)
//                     //     animator.SetBool("Open", true);
//                 }
//             }
//         }
//
//         if (hold)
//         {
//             hit.collider.gameObject.transform.position = holdPoint.position;
//             
//             
//         }
//
//     }
//
//     private void OnDrawGizmos()
//     {
//         Gizmos.color = Color.red;
//         Gizmos.DrawLine(transform.position,transform.position + Vector3.right * transform.localScale.x * distance);
//     }
// }
