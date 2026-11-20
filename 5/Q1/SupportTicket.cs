using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1
{
    public class SupportTicket : Ticket, IAssignable
    {
        public string Department { get; set; }
        public SupportTicket(string ticketCode, string title, int maxHours, int currentHours, string department)
            : base(ticketCode, title, maxHours, currentHours)
        {
            Department = department;
        }
        public override string GetTicketType()
        {
            return "Support";
        }
        public bool AssignHours(int quantity)
        {
            if(quantity <= 0)
            {
                throw new ArgumentException("Quantity must be positive");
            }
            if(CurrentHours + quantity > MaxHours)
            {
                return false;
            }
            CurrentHours += quantity;
            return true;
        }
    }
}
