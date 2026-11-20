using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1
{
    public interface IRegisterable
    {
        public int CurrentEnrollment { get; }
        public bool RegisterStudent(int quantity);
    }
}
