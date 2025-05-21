using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Animator")]
    private Animator _animator;
    public Animator _handAnimator; // get value from hands animator
    
    [Header("Attacking")]
    public float attackDistance = 3f;
    public float attackDelay;
    public float attackSpeed = 1f;
    public int attackDamage = 1;
    public LayerMask attackLayer;

    public GameObject hitEffect;
    public AudioClip swordSwing;
    public AudioClip hitSound;

    bool attacking = false;
    bool readyToAttack = true;
    int attackCount;
    
    
    [Header("Camera")]
    public Camera cam;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }

        //Animator stuff
        _animator.SetBool("isPunching", attacking);
        _handAnimator.SetBool("isHandPunching", attacking);
        _handAnimator.SetBool("isHandIdle", !attacking);
    }

    public void Attack()
    {
        if (!readyToAttack || attacking) return;

        readyToAttack = false;
        attacking = true;

        Invoke(nameof(ResetAttack), attackSpeed);
        Invoke(nameof(AttackRaycast), attackDelay);

        //audioSource.pitch = Random.Range(0.9f, 1.1f);
        //audioSource.PlayOneShot(swordSwing);

        if (attackCount == 0)
        {
            attackCount++;
        }
        else
        {
            attackCount = 0;
        }
    }

    void ResetAttack()
    {
        attacking = false;
        readyToAttack = true;
    }

    void AttackRaycast()
    {
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, attackDistance, attackLayer))
        { 
            HitTarget(hit.point);

            if (hit.transform.TryGetComponent<TestNPC>(out TestNPC T))
            {
                T.TakeDamage(attackDamage);
            }
        } 
    }

    void HitTarget(Vector3 pos)
    {
        //audioSource.pitch = 1;
        //audioSource.PlayOneShot(hitSound);

        //GameObject GO = Instantiate(hitEffect, pos, Quaternion.identity);
        //Destroy(GO, 20);
    }
}
