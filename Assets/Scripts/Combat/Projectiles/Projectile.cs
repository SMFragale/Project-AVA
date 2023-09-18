using UnityEngine;
using AVA.Effects;
using System.Collections.Generic;
using UnityEngine.Events;
using AVA.Core;

namespace AVA.Combat
{
    /// <summary>
    /// Base class for projectiles
    /// </summary>
    /// 
    public abstract class Projectile : MonoBehaviour, Attack
    {
        #region Fields

        [SerializeField]
        protected AudioClip shootSound;

        [SerializeField]
        [Range(0, 100)]
        private int initialPierce = 0;
        private int pierceCount = 0;

        protected List<IBaseEffectFactory> _onHitEffects = new();

        [field: SerializeField]
        public UnityEvent<Vector3> OnProjectileShoot { get; private set; } = new();

        [field: SerializeField]
        public UnityEvent OnProjectileDestroy { get; private set; } = new();
        [field: SerializeField]
        public UnityEvent<ProjectileHitInfo> OnProjectileHit { get; private set; } = new();

        protected Vector3 Direction { get; private set; }

        public AttackInstance AttackInstance { get; private set; }

        #endregion


        #region API
        /// <summary>
        /// Shoots the projectile in the given direction
        /// </summary>
        /// <param name="direction">The Vector3 representing the direction to shoot the projectile</param>
        /// <param name="onHitEffects">The list of effects to apply on hit</param>
        public virtual void LaunchProjectile(Vector3 direction, AttackInstance attackInstance, List<IBaseEffectFactory> onHitEffects = null)
        {
            AttackInstance = attackInstance;
            _onHitEffects = onHitEffects;
            OnProjectileShoot?.Invoke(direction);
            Direction = direction.normalized;
            OnShoot();
        }

        #endregion

        #region Inherited API

        /// <summary>
        /// Must be called when the projectile collides with something. This class is responsible for what happens after the collision, the sub classes are responsible for defining the collision method.
        /// <param name="other"/> is the collider that the projectile collided with.
        /// </summary>
        protected void OnCollision(ProjectileHitInfo projectileHitInfo)
        {
            Debug.Log("Projectile hit somethin");
            if (LayerManager.IsInLayerMask(LayerManager.EnvironmentLayer | LayerManager.GroundLayer, projectileHitInfo.GameObject.layer))
            {
                Debug.Log("Projectile hit environment");
                OnProjectileHit?.Invoke(projectileHitInfo);
                ReturnToPool();
            }
            else if (LayerManager.IsInLayerMask(LayerManager.PierceLayer, projectileHitInfo.GameObject.layer))
            {
                OnProjectileHit?.Invoke(projectileHitInfo);
                CollideTarget(projectileHitInfo.GameObject);

                if (_onHitEffects != null)
                {
                    ApplyEffects(projectileHitInfo.GameObject);
                }
            }
        }


        /// <summary>
        /// Returns the projectile to the pool (if it exists)
        /// </summary>
        protected void ReturnToPool()
        {
            gameObject.SetActive(false);
            OnProjectileDestroy?.Invoke();
            OnProjectileHit.RemoveAllListeners();
        }

        protected virtual void OnStart() { }
        protected virtual void OnUpdate() { }
        protected virtual void OnShoot() { }

        #endregion

        #region Control
        private void ApplyEffects(GameObject other)
        {
            if (other.TryGetComponent<EffectService>(out var effectService))
            {
                foreach (var effect in _onHitEffects)
                    effectService.AddEffect(effect);
            }
        }

        private void CollideTarget(GameObject other)
        {
            if (other.TryGetComponent<CombatTarget>(out var combatTarget))
                combatTarget.TakeDamage(AttackInstance);

            if (pierceCount > 0)
            {
                pierceCount--;
            }
            else
            {
                ReturnToPool();
            }
        }

        private void Start()
        {
            OnStart();
        }
        private void Update()
        {
            OnUpdate();
        }

        private void OnEnable()
        {
            pierceCount = initialPierce;
        }
        #endregion
    }

    public class ProjectileHitInfo
    {
        public GameObject GameObject { get; private set; }
        public Vector3 ContactPoint { get; private set; }
        public Vector3 ContactNormal { get; private set; }

        public ProjectileHitInfo(GameObject gameObject, Vector3 contactPoint, Vector3 contactNormal)
        {
            GameObject = gameObject;
            ContactPoint = contactPoint;
            ContactNormal = contactNormal;
        }
    }
}