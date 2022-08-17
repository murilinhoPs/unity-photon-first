
using Utils;

namespace Enemies
{
    public class EnemyDifficulty : StaticInstance<EnemyDifficulty>
    {
        public float speed = 2.6f;
        public float speedMultiplier = 0.05f;
        
        public float damage = 1.8f;
        public float damageMultiplier = 0.02f;
    }
}