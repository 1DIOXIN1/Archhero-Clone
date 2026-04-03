using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.Movement
{
    public class RigidbodyRotationSystem : IInitializableSystem, IUpdatableSystem
    {
        private ReactiveVariable<Vector3> _moveDirection;
        private Rigidbody _rigidbody;
        
        public void OnInit(Entity entity)
        {
            _rigidbody = entity.Rigidbody;
            _moveDirection = entity.MoveDirection;
        }

        public void OnUpdate(float deltaTime)
        {
            if(_moveDirection.Value == Vector3.zero)//Возможно костыль
                return;
            
            Quaternion lookRotation = Quaternion.LookRotation(_moveDirection.Value);
            
            _rigidbody.MoveRotation(lookRotation);
        }
    }
}