using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1
{
    public class YogaClass : FitnessClass, IEnrollable
    {
        public string InstructorName { get; set; }

        public YogaClass(string classCode, string className, int maxParticipants, int currentParticipants, string instructorName)
            : base(classCode, className, maxParticipants, currentParticipants)
        {
            InstructorName = instructorName;
        }

        public override string GetClassType()
        {
            return "Yoga";
        }

        public bool EnrollParticipants(int quantity)
        {
            if (quantity <= 0)
            {
                throw new ArgumentException("Enrollment quantity must be positive");
            }

            if (quantity > MaxParticipants - CurrentParticipants)
            {
                return false;
            }

            CurrentParticipants += quantity;
            return true;
        }
    }
}
