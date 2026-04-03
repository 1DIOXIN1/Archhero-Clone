using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Infrastructure.DI;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay
{
    public class TestGameplay : MonoBehaviour
    {
        private DIContainer _container;
        private bool _isRunning = false;
        private EntitiesFactory _entitiesFactory;

        private Entity _rigidbodyEntity;
        private Entity _characterControllerEntity;
        
        public void Initialize(DIContainer container)
        {
            _container = container;
            _entitiesFactory = _container.Resolve<EntitiesFactory>();
        }

        public void Run()
        {
            _rigidbodyEntity = _entitiesFactory.CreateRigidbodyEntity(Vector3.zero);
            _characterControllerEntity = _entitiesFactory.CreateCharacterControllerEntity(Vector3.zero + new Vector3(0f, 3f, 0f));
            
            _isRunning = true;
        }
        
        private void Update()
        {
            if (_isRunning == false)
                return;
            
            Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            
            _rigidbodyEntity.MoveDirection.Value = input;
            _characterControllerEntity.MoveDirection.Value = input;
        }
    }
}