using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1
{
    public abstract class FitnessClass
    {
        public string ClassCode { get; set; }
        public string ClassName { get; set; }
        public int MaxParticipants { get; set; }
        public int CurrentParticipants { get; set; }
        protected FitnessClass(string classCode, string className, int maxParticipants)
        {
            if (MaxParticipants <= 0)
            {
                throw new ArgumentOutOfRangeException("MaxParticipants must be greater than 0");
            }

            if (CurrentParticipants < 0 && CurrentParticipants > MaxParticipants)
            {
                throw new ArgumentOutOfRangeException("CurrentParticipants must be between 0 and MaxParticipants");
            }
            classCode = classCode;
            className = className;
            maxParticipants = maxParticipants;
            CurrentParticipants = CurrentParticipants;
        }
        public abstract string GetClassType();
    }
}
