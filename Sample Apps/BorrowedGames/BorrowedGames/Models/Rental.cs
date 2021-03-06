using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oak;

namespace BorrowedGames.Models
{
    public class Rental : Gemini
    {
        public Rental(dynamic game)
        {
            _.Id = game.GameId;
            _.Name = game.Name;
            _.Console = game.Console;
            _.RequestedBy = game.RequestedBy().Select("Id", "Handle");
            _.ReturnDate = game.ReturnDate;
        }
    }
}
