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

        protected FitnessClass(string classCode, string className, int maxParticipants, int currentParticipants)
        {
            if (maxParticipants <= 0)
            {
                throw new ArgumentOutOfRangeException(null, "MaxParticipants must be greater than 0");
            }

            if (currentParticipants < 0 || currentParticipants > maxParticipants)
            {
                throw new ArgumentOutOfRangeException(null, "CurrentParticipants must be between 0 and MaxParticipants");
            }

            ClassCode = classCode;
            ClassName = className;
            MaxParticipants = maxParticipants;
            CurrentParticipants = currentParticipants;
        }

        public abstract string GetClassType();
    }
}