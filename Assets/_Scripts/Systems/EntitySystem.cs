using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;

public class EntitySystem : Singleton<EntitySystem>
{
    //
    // Creator, View and System functionalities merged - to be refactorized later
    //

    [SerializeField] BoardFieldsGizmo _boardFieldsGizmo;
    [SerializeField] EntityView _emptyEntityViewPrefab;
    [SerializeField] EntityView _minionEntityViewPrefab;

    List<EntityView> _entities = new();
    Dictionary<int, EntityView> _hiddenEmptyEntities = new();

    public List<EntityView> All => _entities;

    void OnEnable()
    {
        ActionSystem.AttachPerformer<CreateEntityGA>(CreateEntityPerformer);
        ActionSystem.AttachPerformer<ChangeStatsGA>(ChangeStatPerformer);
        ActionSystem.AttachPerformer<DealDamageGA>(DealDamagePerformer);
        ActionSystem.AttachPerformer<DestroyEntityGA>(DestroyEntityPerformer);
        ActionSystem.AttachPerformer<AttackGA>(AttackPerformer);
    }
    void OnDisable()
    {
        ActionSystem.DetachPerformer<CreateEntityGA>();
        ActionSystem.DetachPerformer<ChangeStatsGA>();
        ActionSystem.DetachPerformer<DealDamageGA>();
        ActionSystem.DetachPerformer<DestroyEntityGA>();
        ActionSystem.DetachPerformer<AttackGA>();
    }

    IEnumerator CreateEntityPerformer(CreateEntityGA createEntityGA)
    {
        EntityView target = createEntityGA.TargetEntity;
        if (!target)
        {
            Debug.Log("Create entity action fizzled (pecefully), cause: no target");
            yield break;
        }
        var newEntity = Instantiate(_minionEntityViewPrefab, target.transform.position, target.transform.rotation); // TODO generalize and automate prefab selection
        newEntity.Setup(createEntityGA.CardInstance, target.Side);

        createEntityGA.TargetEntity = newEntity;

        int index = _entities.IndexOf(target);
        if (index != -1)
        {
            _entities[index] = newEntity;
            _hiddenEmptyEntities.Add(index, target);
        }
        else
        {
            Debug.LogWarning($"Entity {target} was not found in EntitySystem._entities");
        }
        target.gameObject.SetActive(false);
        Debug.Log("Entity Created");
        yield return null;
    }

    public void InitializeEmptyEntities()
    {
        (List<Vector3> sideA, List<Vector3> sideB) = _boardFieldsGizmo.GetFieldPositions();
        foreach (Vector3 pos in sideA)
        {
            EntityView newEntity = Instantiate(_emptyEntityViewPrefab, pos, Quaternion.Euler(90,0,0));
            newEntity.Setup(null, Side.A);
            _entities.Add(newEntity);
        }
        foreach (Vector3 pos in sideB)
        {
            EntityView newEntity = Instantiate(_emptyEntityViewPrefab, pos, Quaternion.Euler(90, 0, 0));
            newEntity.Setup(null, Side.B);
            _entities.Add(newEntity);
        }
    }

    IEnumerator ChangeStatPerformer(ChangeStatsGA changeStatsGA)
    {
        foreach (EntityView target in changeStatsGA.Targets)
        {
            if (target is MinionEntityView minion) // TODO interface if more entity types have stats
            {
                minion.ApplyStatChanges(changeStatsGA.Stat1Change, changeStatsGA.Stat2Change, changeStatsGA.Stat3Change);
            }
            else
            {
                Debug.LogWarning($"Cannot change stats of {target} entity");
            }
        }
        yield return null;
    }

    IEnumerator DealDamagePerformer(DealDamageGA dealDamageGA)
    {
        foreach (EntityView target in dealDamageGA.Targets)
        {
            if (target is IDamagable damagable) {
                if (damagable.TakeDamage(dealDamageGA.Amount))
                {
                    DestroyEntityGA destroyEntityGA = new(target);
                    ActionSystem.Instance.AddReaction(destroyEntityGA);
                }
            }
            else
            {
                Debug.LogWarning($"target {target} is not damagable");
            }
        }
        yield return null;
    }

    IEnumerator DestroyEntityPerformer(DestroyEntityGA destroyEntityGA)
    {
        foreach (EntityView target in destroyEntityGA.Targets)
        {
            int index = _entities.IndexOf(target);
            if (index != -1)
            {
                _entities[index] = _hiddenEmptyEntities[index];
                _entities[index].gameObject.SetActive(true);
                _hiddenEmptyEntities.Remove(index);
            }
            else
            {
                Debug.LogWarning($"Entity {target} was not found in EntitySystem._entities");
            }
            Destroy(target.gameObject);
        }
        yield return null;
    }

    IEnumerator AttackPerformer(AttackGA attackGA)
    {
        if (!(attackGA.EntitySource is MinionEntityView attacker)) yield break;
        Vector3 returnPos = attacker.transform.position;
        Quaternion returnRot = attacker.transform.rotation;
        var seq = DOTween.Sequence();
        foreach (EntityView target in attackGA.Targets)
        {
            // TODO consider delegating this movement to wrapper
            seq.Append(
                attacker.transform.DOMove(
                    Vector3.Lerp(returnPos, target.transform.position, 0.9f),
                    AnimConfig.AttackAnimTime
                )
            );
            seq.AppendCallback(() =>
            {
                DealDamageGA dealDamageGA = new(attacker, attacker.Stat1, new() { target });
                ActionSystem.Instance.AddReaction(dealDamageGA);
                // TODO this isn't sensitive to to tagets that dont deal dmg (eg. players), Interface may me needed
                if (target is MinionEntityView minionTarget) 
                { 
                    DealDamageGA returnDamageGA = new(target, minionTarget.Stat1, new() { attacker }); 
                    ActionSystem.Instance.AddReaction(returnDamageGA);
                }
                    //ActionSystem.Instance.AddReaction(new DealDamageGA(attacker, attacker.Stat1, new() { target }));
                    //ActionSystem.Instance.AddReaction(new DealDamageGA(target, attacker.Stat1, new() { attacker }));
            });
            seq.Append(attacker.transform.DOMove(returnPos, AnimConfig.AttackAnimTime));
        }
        yield return seq.WaitForCompletion();
    }
}
