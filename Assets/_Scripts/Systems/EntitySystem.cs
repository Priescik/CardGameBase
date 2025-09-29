using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    //public List<EntityView> All => _entities;

    void OnEnable()
    {
        ActionSystem.AttachPerformer<CreateEntityGA>(CreateEntityPerformer);
        ActionSystem.AttachPerformer<ChangeStatsGA>(ChangeStatPerformer);
        ActionSystem.AttachPerformer<DealDamageGA>(DealDamagePerformer);
        ActionSystem.AttachPerformer<DestroyEntityGA>(DestroyEntityPerformer);
    }
    void OnDisable()
    {
        ActionSystem.DetachPerformer<CreateEntityGA>();
        ActionSystem.DetachPerformer<ChangeStatsGA>();
        ActionSystem.DetachPerformer<DealDamageGA>();
        ActionSystem.DetachPerformer<DestroyEntityGA>();
    }

    IEnumerator CreateEntityPerformer(CreateEntityGA createEntityGA)
    {
        EntityView target = createEntityGA.TargetEntity;
        var newEntity = Instantiate(_minionEntityViewPrefab, target.transform.position, target.transform.rotation); // TODO generalize and automate prefab selection
        newEntity.Setup(createEntityGA.CardInstance);

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

        yield return null; // TODO animation
    }

    public void InitializeEmptyEntities()
    {
        foreach (Vector3 pos in _boardFieldsGizmo.GetFieldPositions())
        {
            EntityView newEntity = Instantiate(_emptyEntityViewPrefab, pos, Quaternion.Euler(90,0,0));
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
            else if (!(target is EmptyEntityView))
            {
                Debug.Log($"Cannot change stats of {target} entity");
                // TODO handle non-minion targets (which shouldn't ever be the targets)
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
                    //Destroy(target.gameObject); // TODO performer + GA
                    DestroyEntityGA destroyEntityGA = new(target);
                    ActionSystem.Instance.AddReaction(destroyEntityGA);
                }
            }
            else
            {
                // TODO handle non-minion targets (which shouldn't ever be the targets)
                Debug.Log($"target {target} is not damagable");
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
            Destroy(target.gameObject); // TODO store and restore empty prefab in case of entity destruction
        }
        yield return null;
    }
}
