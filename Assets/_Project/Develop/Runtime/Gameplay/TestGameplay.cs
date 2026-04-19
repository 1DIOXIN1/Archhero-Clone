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

        private Entity _hero;
        private Entity _ghost;
        private Entity _characterControllerEntity;
        
        public void Initialize(DIContainer container)
        {
            _container = container;
            _entitiesFactory = _container.Resolve<EntitiesFactory>();
        }

        public void Run()
        {
            _hero = _entitiesFactory.CreateHero(Vector3.zero);
            _ghost = _entitiesFactory.CreateGhost(Vector3.zero + new Vector3(0f, 0f, -3f));
            _characterControllerEntity = _entitiesFactory.CreateCharacterControllerEntity(Vector3.zero + new Vector3(0f, 0f, 3f));
            
            _isRunning = true;
        }
        
        private void Update()
        {
            if (_isRunning == false)
                return;
            
            Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            
            _hero.MoveDirection.Value = input;
            _hero.RotationDirection.Value = input;
            _characterControllerEntity.MoveDirection.Value = input;
 
            if (Input.GetKeyDown(KeyCode.Space))
                _hero.TakeDamageRequest.Invoke(50);
            
            if (Input.GetKeyDown(KeyCode.R))
                _hero.StartAttackRequest.Invoke();
        }
    }
}