using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.Movement
{
    public class TransformRotationSystem : IInitializableSystem, IUpdatableSystem
    {
        private ReactiveVariable<Vector3> _moveDirection;
        private Transform _transform;
        
        public void OnInit(Entity entity)
        {
            _moveDirection = entity.MoveDirection;
            _transform = entity.Transform;
        }

        public void OnUpdate(float deltaTime)
        {
            if(_moveDirection.Value == Vector3.zero)//Возможно костыль
                return;
            
            Quaternion rotation = Quaternion.LookRotation(_moveDirection.Value);
            
            _transform.rotation = rotation;
        }
    }
}