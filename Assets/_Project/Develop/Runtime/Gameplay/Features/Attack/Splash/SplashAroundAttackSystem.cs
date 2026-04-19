using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Gameplay.Features.ApplyDamage;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace _Project.Develop.Runtime.Gameplay.Features.Attack.Splash
{
    public class SplashAttackAroundSystem : IInitializableSystem, IDisposableSystem
    {
        private CollidersRegistryService _collidersRegistryService;
        
        private Transform _transform;
        private List<Collider> _contacts = new();
        private ReactiveVariable<float> _splashRadius;
        private ReactiveVariable<float> _damage;

        private ReactiveEvent _teleportEvent;

        private IDisposable _teleportDisposable;

        public SplashAttackAroundSystem(CollidersRegistryService collidersRegistryService)
        {
            _collidersRegistryService = collidersRegistryService;
        }

        public void OnInit(Entity entity)
        {
            _transform = entity.Transform;
            _splashRadius = entity.InstantSplashAttackRadius;
            _damage = entity.InstantSplashAttackDamage;
            
            _teleportEvent = entity.TeleportEvent;

            _teleportDisposable = _teleportEvent.Subscribe(OnTeleported);
        }

        private void OnTeleported()
        {            
            _contacts = Physics.OverlapSphere(_transform.position, _splashRadius.Value).ToList();

            foreach (var contact in _contacts)
            {
                Entity contactEntity = _collidersRegistryService.GetBy(contact);

                if (contactEntity != null)
                {
                    if(contactEntity.HasComponent<TakeDamageRequest>())
                        contactEntity.TakeDamageRequest.Invoke(_damage.Value);
                }
            }
        }

        public void OnDispose()
        {
            _teleportDisposable.Dispose();
        }
    }
}
