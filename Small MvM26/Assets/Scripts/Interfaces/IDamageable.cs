using System;
using UnityEngine;

namespace SmallGame
{
    public interface IDamageable
    {
        public float Health { get; }
        public void ApplyDamage(float amount, Transform source);
        public event Action<float> OnHealthChanged;

    }
}