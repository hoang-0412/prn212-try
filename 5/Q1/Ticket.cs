using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1
{
    public abstract class Ticket
    {
        public string TicketCode { get; set; }
        public string Title { get; set; }
        public int MaxHours { get; set; }
        public int CurrentHours { get; set; }
        public Ticket(string ticketCode, string title, int maxHours, int currentHours)
        {
            if (maxHours <= 0)
            {
                throw new ArgumentOutOfRangeException("MaxHours must be greater than 0");
            }
            if(currentHours < 0 || currentHours > maxHours)
            {
                throw new ArgumentOutOfRangeException("CurrentHours must be between 0 and MaxHours");
            }
            TicketCode = ticketCode;
            Title = title;
            MaxHours = maxHours;
            CurrentHours = currentHours;
        }
        public abstract string GetTicketType();
    }
}
