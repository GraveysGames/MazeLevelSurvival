using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class ProjectileController : NetworkBehaviour
{
    [SerializeField] private Transform _projectileStartingTransform;
    [SerializeField] private GameObject _projectilePrefab;
    private bool isPaused = false;


    [SerializeField] private float warmup;
    private float warmUpProgress;
    [SerializeField] private bool ifWarmUpDownResets;
    [SerializeField] private float cooldown;
    private float coolDownProgress;
    //warmup
    //cooldown

    private void Start()
    {
        UserInterfaceEvents.Singleton.OnPauseMenu += PauseUnPause;
    }

    private void Update()
    {
        if (IsOwner)
        {
            if (isPaused)
            {
                return;
            }
            ProjectileUpdate();
        }
    }

    protected virtual void ProjectileUpdate()
    {

        if (warmup > 0)
        {
            WarmUpControll();
        }
        else
        {
            FireWithCooldown();
        }
    }

    protected virtual void WarmUpControll()
    {
        if (Input.GetMouseButton(0))
        {
            if (warmUpProgress <= 0)
            {
                if (ifWarmUpDownResets)
                {
                    warmUpProgress = warmup;
                }
                FireWithCooldown();
            }
            else
            {
                warmUpProgress -= Time.deltaTime;
            }
        }
        else
        {
            warmUpProgress = warmup;
        }
    }

    protected virtual void FireWithCooldown()
    {
        coolDownProgress -= Time.deltaTime;

        if (Input.GetMouseButton(0) && (coolDownProgress <= 0))
        {
            FireProjectile();
            coolDownProgress = cooldown;
        }

    }


    GameObject projectile;
    public void FireProjectile()
    {
        //projectile = Instantiate(_projectilePrefab, _projectileStartingTransform.position, _projectileStartingTransform.rotation);

        SpawnProjectileSelf(_projectileStartingTransform.position, _projectileStartingTransform.rotation);
        //SpawnProjectileGhost(_projectileStartingTransform.position, _projectileStartingTransform.rotation);

        FireProjectileRelayServerRPC(_projectileStartingTransform.position, _projectileStartingTransform.rotation);

    }

    private void SpawnProjectileSelf(Vector3 position, Quaternion rotation)
    {
        projectile = Instantiate(_projectilePrefab, position, rotation);
    }

    private void SpawnProjectileGhost(Vector3 position, Quaternion rotation)
    {
        projectile = Instantiate(_projectilePrefab, position, rotation);
        //fix
        projectile.GetComponentInChildren<BulletController>().isGhost = true;
    }

    [ServerRpc]
    private void FireProjectileRelayServerRPC(Vector3 position, Quaternion rotation)
    {
        FireProjectileClientRPC(position, rotation);
    }

    [ClientRpc]
    private void FireProjectileClientRPC(Vector3 position, Quaternion rotation)
    {
        if (IsOwner)
        {
            return;
        }
        else
        {
            SpawnProjectileGhost(position, rotation);
        }
    }

    private void PauseUnPause(bool isPaused) 
    {
        this.isPaused = isPaused;
    }

}
