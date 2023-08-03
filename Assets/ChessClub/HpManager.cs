using utils;

namespace ChessClub
{
    public class HpManager
    {
        private Ihp _boss;
        private Ihp _player;

        private HpManager(Ihp player, Ihp boss )
        {
            _player = player;
            _boss = boss;
        }
    }
}