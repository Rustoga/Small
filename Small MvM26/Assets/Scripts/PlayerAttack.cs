using System;
using SmallGame.Input;
using UnityEngine;

namespace SmallGame
{
    public class PlayerAttack : MonoBehaviour
    {
        InputHandler _input;
        [SerializeField] Transform _bulletSpawn;
        [SerializeField] GameObject _bulletObject;
        void Start()
        {
            _input = InputHandler.Instance;
            _input.OnAttack.AddListener(UpdateAttack);
        }

        void OnDestroy()
        {
            _input.OnAttack.RemoveListener(UpdateAttack);
        }

        private void UpdateAttack(float pressed)
        {
            if (pressed == 0)
                return;

            GameObject bullet = Instantiate(_bulletObject, _bulletSpawn.position, Quaternion.identity);
            Vector2 direction = new Vector2(transform.localScale.x, 0);
            bullet.GetComponent<Bullet>().EnableBullet(direction);

        }
    }
}
