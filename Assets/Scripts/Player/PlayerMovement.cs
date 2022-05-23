using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace RPG
{
    public class PlayerMovement : MonoBehaviour
    {
        Animator animator;
        public float moveSpeed = 100.0f;
        public float rotateSpeed = 150.0f;
        NavMeshAgent navMesh;
        Rigidbody rigidbody;
        PlayerAttack playerAttack;

        void Awake()
        {
            navMesh = GetComponent<NavMeshAgent>();
            rigidbody = GetComponent<Rigidbody>();
            animator = GetComponentInChildren<Animator>();
            playerAttack = GetComponent<PlayerAttack>();
        }

        private void Start()
        {
            playerAttack.animEvent.attackStartAction += OnAttack;
            playerAttack.animEvent.skillStartAction += OnAttack;
            playerAttack.animEvent.skillEndAction += EndAttack;
            playerAttack.animEvent.attackEndAction += EndAttack;
        }

        // Update is called once per frame
        void Update()
        {
            KeyboardMove();
            MouseMove();
            AnimatorMove();
        }

        void KeyboardMove()
        {
            Vector2 input = new Vector2(InputManager.Instance.xMove, InputManager.Instance.yMove);
            if (input.magnitude <= 0)
            {
                return;
            }
            if (navMesh.isStopped == true)
            {
                return;
            }


            if (Mathf.Abs(input.x) > Mathf.Epsilon || Mathf.Abs(input.y) > Mathf.Epsilon)
            {
                NavMeshHit hit;

                // x, y(z)축 상대적으로 움직이는 거리 계산
                Vector3 MoveXDistance = input.x * Camera.main.gameObject.transform.right * moveSpeed;
                Vector3 MoveYDistance = input.y * Camera.main.gameObject.transform.forward * moveSpeed;
                // x y 움직인 Vector를 하나에 합친다
                Vector3 MoveVec = transform.position + MoveXDistance + MoveYDistance;
                MoveVec.y = 0;
                if (NavMesh.SamplePosition(MoveVec, out hit, 999.0f, NavMesh.AllAreas))
                {

                    navMesh.destination = hit.position;
                    Rotation(hit.position - transform.position);
                }
            }
        }

        void MouseMove()
        {

        }

        void Rotation(Vector3 dir)
        {
            dir.y = 0;
            dir.Normalize();
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), rotateSpeed * Time.smoothDeltaTime);
        }

        void AnimatorMove()
        {
            animator.SetFloat("Speed", navMesh.velocity.magnitude / navMesh.speed);
        }

        void OnAttack()
        {
            navMesh.isStopped = true;
        }

        void EndAttack()
        {
            navMesh.isStopped = false;
            navMesh.SetDestination(transform.position);
        }
    }
}

