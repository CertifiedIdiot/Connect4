// -----------------------------------------------------------------------------------------------
//  IPlayer.cs by Thomas Thorin, Copyright (C) 2022.
//  Published under GNU General Public License v3 (GPL-3)
// -----------------------------------------------------------------------------------------------

namespace Connect4.Models
{
    using Connect4.Enums;

    public interface IPlayer
    {
        Colors Color { get; set; }
        string Name { get; set; }
    }
}