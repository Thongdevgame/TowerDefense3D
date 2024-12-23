using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageReceiver : DamageReceiver
{
    [SerializeField] protected EnemyCtrl ctrl;
    [SerializeField] protected CapsuleCollider capsuleCollider;
    
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCtrl();
        this.LoadCapsuleCollider();
    }

    protected virtual void LoadCapsuleCollider()
    {
        if (this.capsuleCollider != null) return;
        this.capsuleCollider = GetComponent<CapsuleCollider>();
        this.capsuleCollider.center = new Vector3(0,0,0);
        this.capsuleCollider.radius = 0.3f;
        this.capsuleCollider.height = 1.5f;
        this.capsuleCollider.isTrigger = true;
        Debug.Log(transform.name + ": capsuleCollider", gameObject);
    }
    protected virtual void LoadCtrl()
    {
        if (this.ctrl != null) return;
        this.ctrl = transform.GetComponentInParent<EnemyCtrl>();
        Debug.Log(transform.name + ": LoadCtrl", gameObject);
    }

    protected virtual void DoDespawn()
    {
        this.ctrl.Despawn.DoDespawn();

    }
    protected override void OnDead()
    {
        this.ctrl.Agent.isStopped = true;   
        this.ctrl.Animator.SetBool("isDead", this.isDead);
        this.capsuleCollider.enabled = false;
        Invoke(nameof(this.DoDespawn), 5f);

        ItemDropSpawnerCtrl.Instance.DropMany(ItemCode.Gold, transform.position, 10);
        ItemDropSpawnerCtrl.Instance.DropMany(ItemCode.Diamon, transform.position, 2);
        ItemDropSpawnerCtrl.Instance.Drop(ItemCode.Wand, transform.position, 1);
        ItemDropSpawnerCtrl.Instance.Drop(ItemCode.Shotgun, transform.position, 1);
        ItemDropSpawnerCtrl.Instance.Drop(ItemCode.Piston, transform.position, 1);
        ItemDropSpawnerCtrl.Instance.Drop(ItemCode.Bow, transform.position, 1);
        InventoriesManager.Instance.AddItem(ItemCode.PlayerExp, 1);
    }
    public override void Receiver(int damage, DamageSender damageSender)
    {
        base.Receiver(damage, damageSender);
        this.OnHurt();
    }
   
    protected override void OnHurt()
    {
        if (this.ctrl.Animator.GetBool("isHit")) return;

       // this.ctrl.Agent.isStopped = true; 
        this.ctrl.Animator.SetBool("isHit", true); 

        StartCoroutine(ResetHitState());
    }

    private IEnumerator ResetHitState()
    {
        yield return new WaitForSeconds(0.3f); 
        if (!this.IsDead())
        {
            this.ctrl.Agent.isStopped = false;
            this.ctrl.Animator.SetBool("isHit", false); 
        }
    }

    protected override void Reborn()
    {
        base.Reborn();
        this.capsuleCollider.enabled = true;
    }
}
