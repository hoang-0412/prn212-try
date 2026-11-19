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
            // 1. Tạo ClassManager với callback in ra màn hình
            ClassManager manager = new ClassManager((className, currentParticipants) =>
            {
                Console.WriteLine($"[CLASS]: {className} now has {currentParticipants}");
            });

            // 2. Tạo dữ liệu mẫu và thêm vào manager
            YogaClass c1 = new YogaClass("F001", "Morning Yoga", 25, 12, "Anna");
            YogaClass c2 = new YogaClass("F002", "Power Yoga", 20, 10, "Brian");
            YogaClass c3 = new YogaClass("F003", "Stretching Basics", 30, 18, "Cindy");

            manager.AddClass(c1);
            manager.AddClass(c2);
            manager.AddClass(c3);

            // 3. Thực hiện 2 lượt đăng ký thành công
            manager.EnrollClass("F001", 3);
            manager.EnrollClass("F002", 2);

            // 4. Đăng ký vượt quá số lượng tối đa
            bool enrollResult = manager.EnrollClass("F002", 9);
            Console.WriteLine($"Enrollment result for F002 +9: {enrollResult}");

            // 5. Bắt lỗi ArgumentException (quantity <= 0)
            try
            {
                manager.EnrollClass("F001", 0);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            // 6. Bắt lỗi InvalidOperationException (mã lớp không tồn tại)
            try
            {
                manager.EnrollClass("F999", 2);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            // 7. Bắt lỗi ArgumentOutOfRangeException (MaxParticipants <= 0)
            try
            {
                YogaClass invalidMax = new YogaClass("F004", "Test Yoga", 0, 0, "David");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            // 8. Bắt lỗi ArgumentOutOfRangeException (CurrentParticipants > MaxParticipants)
            try
            {
                YogaClass invalidCurrent = new YogaClass("F005", "Test Yoga", 10, 11, "Eva");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            // 9. Lọc và in danh sách lớp còn trống >= 5 chỗ
            Console.WriteLine();
            Console.WriteLine("=== Classes With At Least 5 Open Slots ===");
            var availableClasses = manager.GetClassesWithOpenSlots(5);
            for (int i = 0; i < availableClasses.Count; i++)
            {
                var item = availableClasses[i];
                int openSlots = item.MaxParticipants - item.CurrentParticipants;
                Console.WriteLine($"{i + 1}. {item.ClassName} | Code: {item.ClassCode} | InstructorName: {item.InstructorName} | Available: {openSlots} | Type: {item.GetClassType()}");
            }
        }
    }
}
