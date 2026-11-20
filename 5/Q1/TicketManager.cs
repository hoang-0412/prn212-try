using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1
{
    public class TicketManager
    {
        private List<SupportTicket> _tickets;
        private TicketUpdatedCallback _onTicketUpdated;
        public TicketManager(TicketUpdatedCallback callback)
        {
            _tickets = new List<SupportTicket>();
            _onTicketUpdated = callback;
        }
        public void AddTicket(SupportTicket item)
        {
            if (item != null)
            {
                _tickets.Add(item);
            }
        }
        public bool AssignTicket(string ticketCode, int quantity)
        {
            var item = _tickets.FirstOrDefault(t => t.TicketCode == ticketCode);
            if (item == null)
            {
                throw new InvalidOperationException($"Ticket not found : {ticketCode}");
            }
            bool isSuccess = item.AssignHours(quantity);
            if(isSuccess)
            {
                _onTicketUpdated?.Invoke(item.Title, item.CurrentHours);
            }
            return isSuccess;
        }
        public List<SupportTicket> GetTicketsWithRemainingHours(int minHours)
        {
            return _tickets.Where(t => (t.MaxHours - t.CurrentHours) >= minHours).OrderByDescending(t => t.MaxHours - t.CurrentHours).ToList();
        }
    }
}
