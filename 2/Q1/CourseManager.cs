using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1
{
    public class CourseManager
    {
        private List<OnlineCourse> _courses;
        private RegistrationChangedCallback _onRegistrationChanged;

        public CourseManager(RegistrationChangedCallback callback)
        {
            _courses = new List<OnlineCourse>();
            _onRegistrationChanged = callback;
        }

        public void AddCourse(OnlineCourse item)
        {
            _courses.Add(item);
        }

        public bool RegisterCourse(string courseCode, int quantity)
        {
            var course = _courses.FirstOrDefault(c => c.CourseCode == courseCode);

            if (course == null)
            {
                throw new InvalidOperationException($"Course not found: {courseCode}");
            }

            bool isRegistered = course.RegisterStudent(quantity);

            if (isRegistered)
            {
                _onRegistrationChanged?.Invoke(course.Title, course.CurrentEnrollment);
            }

            return isRegistered;
        }

        public List<OnlineCourse> GetAvailableCourses(int minSeats)
        {
            return _courses
                .Where(c => (c.Capacity - c.CurrentEnrollment) >= minSeats)
                .OrderByDescending(c => c.Capacity - c.CurrentEnrollment)
                .ToList();
        }
    }
}