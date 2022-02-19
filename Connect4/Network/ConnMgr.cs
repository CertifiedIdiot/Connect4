using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Network
{
    public class ConnMgr
    {
        RelayUser PlayerLobby { get; set; }
        RelayUser Player { get; set; }
        public ConnMgr(RelayUser PlayerLobby, RelayUser Player)
        {
            this.PlayerLobby = PlayerLobby;
            this.Player = Player;
        }
        

    }
}
