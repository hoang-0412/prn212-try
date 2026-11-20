using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Tạo CourseManager với callback in thông báo ra console
            CourseManager manager = new CourseManager((title, enrollment) =>
            {
                Console.WriteLine($"[REGISTRATION]: {title} now has {enrollment}");
            });

            // Tạo ít nhất 3 đối tượng OnlineCourse
            OnlineCourse c1 = new OnlineCourse("C001", "C# Fundamentals", 30, 20, "Zoom");
            OnlineCourse c2 = new OnlineCourse("C002", "WPF Basics", 30, 15, "Teams");
            OnlineCourse c3 = new OnlineCourse("C003", "Entity Framework Core", 20, 10, "Google Meet");

            // Thêm vào manager
            manager.AddCourse(c1);
            manager.AddCourse(c2);
            manager.AddCourse(c3);

            // Đăng ký thành công
            manager.RegisterCourse("C001", 5);
            manager.RegisterCourse("C002", 3);

            // Trường hợp quantity <= 0 (bắt ArgumentException)
            try
            {
                manager.RegisterCourse("C001", 0);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            // Trường hợp CourseCode không tồn tại (bắt InvalidOperationException)
            try
            {
                manager.RegisterCourse("C999", 2);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            // Gọi GetAvailableCourses(5) và in kết quả
            Console.WriteLine("\n=== Courses With At Least 5 Available Seats ===");
            List<OnlineCourse> availableCourses = manager.GetAvailableCourses(5);
            for (int i = 0; i < availableCourses.Count; i++)
            {
                var c = availableCourses[i];
                int availableSeats = c.Capacity - c.CurrentEnrollment;
                Console.WriteLine($"{i + 1}. {c.Title} | Code: {c.CourseCode} | Platform: {c.Platform} | Available: {availableSeats} | Type: {c.GetCourseType()}");
            }
        }
    }
}