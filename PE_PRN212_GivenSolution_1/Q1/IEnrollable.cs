using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1
{
    public interface IEnrollable
    {
        public int MaxParticipants { get; }
        public int CurrentParticipants { get; }
        public bool EnrollParticipant(int quantity);
    }
}
