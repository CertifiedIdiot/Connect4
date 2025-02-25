﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Connect4.Network
{
    public class RelayUser
    {
        public string Username { get; set; } = string.Empty;
        public Socket ClientSocket { get; set; } = default!;
        public bool LobbyIsOpen { get; set; }
        public string Key { get; set; } = string.Empty;
    }
}
