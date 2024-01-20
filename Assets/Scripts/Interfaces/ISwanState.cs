using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Interfaces
{
    public interface ISwanState
    {
        void MoveUp();
        void MoveDown();
        void MoveLeft();
        void MoveRight();

        void Attack();
        void TakeDamage();
        void Die();
        void Update();
    }
}
