using ChessClub;

namespace utils
{
    public interface Ihp
    {
        public void SetHpManager(HpManager hpManager);
        public void Hurted(float damage);
        public void Heal(float heal);
        public float GetHp();
        public void  SetHp(float hp);
    }
}