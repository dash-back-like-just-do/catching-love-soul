using System.Collections.Generic;
using utils;

namespace ChessClub
{
    
    public class HpManager
    {
        private List<Ihp> Ihps;

        public void addNewIhp(Ihp i)
        {
            Ihps.Add(i);
        }
    }
}