using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1
{
    public class main
    {
        static void Main(string[] args)
        {
            TicketManager manager = new TicketManager((ticketTitle, currentHours) =>
            {
                Console.WriteLine($"[TICKET]: {ticketTitle} now has {currentHours}");
            });
            SupportTicket ticket1 = new SupportTicket("T001", "Login Failure", 12, 4, "IT Helpdesk");
            SupportTicket ticket2 = new SupportTicket("T002", "Printer Issue", 10, 5, "Operations");
            SupportTicket ticket3 = new SupportTicket("T003", "Email Configuration", 8, 2, "Network");
            manager.AddTicket(ticket1);
            manager.AddTicket(ticket2);
            manager.AddTicket(ticket3);
            manager.AssignTicket("T001", 3);
            manager.AssignTicket("T002", 2);
            try
            {
                manager.AssignTicket("T001", 0);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            try
            {
                manager.AssignTicket("T999", 2);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            Console.WriteLine("\n=== Tickets With At Least 5 Remaining Hours ===");
            List<SupportTicket> remainingTickets = manager.GetTicketsWithRemainingHours(5);
            int index = 1;
            foreach (var ticket in remainingTickets)
            {
                int available = ticket.MaxHours - ticket.CurrentHours;
                Console.WriteLine($"{index}. {ticket.Title} | Code: {ticket.TicketCode} | Department: {ticket.Department} | Available: {available} | Type: {ticket.GetTicketType()}");
                index++;
            }
        }
    }
}