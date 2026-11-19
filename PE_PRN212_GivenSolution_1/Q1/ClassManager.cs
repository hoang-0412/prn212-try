using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1
{
    public class ClassManager
    {
        private List<YogaClass> _classes;
        private ClassEnrollmentCallback _onEnrollmentChanged;

        public ClassManager(ClassEnrollmentCallback callback)
        {
            _classes = new List<YogaClass>();
            _onEnrollmentChanged = callback;
        }

        public void AddClass(YogaClass item)
        {
            _classes.Add(item);
        }

        public bool EnrollClass(string classCode, int quantity)
        {
            // So sanh chuoi phan biet hoa thuong
            var item = _classes.FirstOrDefault(c => string.Equals(c.ClassCode, classCode, StringComparison.Ordinal));

            if (item == null)
            {
                throw new InvalidOperationException($"Class not found: {classCode}");
            }

            bool isSuccess = item.EnrollParticipants(quantity);

            if (isSuccess)
            {
                _onEnrollmentChanged?.Invoke(item.ClassName, item.CurrentParticipants);
            }

            return isSuccess;
        }

        public List<YogaClass> GetClassesWithOpenSlots(int minSlots)
        {
            return _classes
                .Where(c => (c.MaxParticipants - c.CurrentParticipants) >= minSlots)
                .OrderByDescending(c => c.MaxParticipants - c.CurrentParticipants)
                .ToList();
        }
    }
}