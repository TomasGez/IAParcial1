using UnityEngine;
using System;
using System.Collections;

public class PreySensor : PreyTriggers
{
    private Coroutine _keepTargetRoutine;

    private void OnTriggerEnter(Collider other)
    {
        if (!IsValidTarget(other.gameObject.layer, _agent.TargetMask) || _agent.IsDead) return;

        if (other.TryGetComponent<Animal>(out var animal))
        {
            if (!animal.IsDead) _agent.PreyAttached(animal);
        }

        if (other.TryGetComponent<Plant>(out var plant)) _agent.BaitEntered(plant);

        if (other.TryGetComponent<Archer>(out var archer)) StopKeepLooking(archer);
    }

    private void StopKeepLooking(Archer target)
    {
        if (_keepTargetRoutine != null)
        {
            StopCoroutine(_keepTargetRoutine);
            _keepTargetRoutine = null;
        }

        _agent.HunterEntered(target);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!IsValidTarget(other.gameObject.layer, _agent.TargetMask) || _agent.IsDead) return;

        if (other.TryGetComponent<Plant>(out var plant)) _agent.BaitExited(plant);

        if (other.TryGetComponent<Archer>(out var archer)) StartKeepLooking(archer);
    }

    private void StartKeepLooking(Archer target)
    {
        if (_keepTargetRoutine == null)
                _keepTargetRoutine = StartCoroutine(KeepLooking(target));
    }

    private IEnumerator KeepLooking(Archer target)
    {
        yield return new WaitForSeconds(_agent.KeepTargetTime);

        _agent.HunterExited(target);
    }
}
