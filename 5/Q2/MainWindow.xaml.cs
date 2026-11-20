using Microsoft.EntityFrameworkCore;
using Q2.Models;
using System.Windows;

namespace Q2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadComboBoxes();
            LoadCourses();
        }

        private void LoadComboBoxes()
        {
            using var context = new Prn2122Context();

            var categories = context.Categories.OrderBy(c => c.CategoryId).ToList();
            var filterCategories = new List<Category>
            {
                new Category { CategoryId = 0, CategoryName = "All" }
            };
            filterCategories.AddRange(categories);
            cbFilterCategory.ItemsSource = filterCategories;
            cbFilterCategory.SelectedIndex = 0;

            cbAddCategory.ItemsSource = categories;
            if (categories.Count > 0)
            {
                cbAddCategory.SelectedIndex = 0;
            }

            var instructors = context.Instructors.OrderBy(i => i.InstructorId).ToList();
            var filterInstructors = new List<Instructor>
            {
                new Instructor { InstructorId = 0, InstructorName = "All" }
            };
            filterInstructors.AddRange(instructors);
            cbFilterInstructor.ItemsSource = filterInstructors;
            cbFilterInstructor.SelectedIndex = 0;

            cbAddInstructor.ItemsSource = instructors;
            if (instructors.Count > 0)
            {
                cbAddInstructor.SelectedIndex = 0;
            }
        }

        private void LoadCourses(int categoryId = 0, int instructorId = 0)
        {
            using var context = new Prn2122Context();

            var query = context.Courses
                .Include(c => c.Category)
                .Include(c => c.Instructor)
                .AsQueryable();

            if (categoryId > 0)
            {
                query = query.Where(c => c.CategoryId == categoryId);
            }

            if (instructorId > 0)
            {
                query = query.Where(c => c.InstructorId == instructorId);
            }

            var list = query
                .OrderBy(c => c.CourseId)
                .Select(c => new
                {
                    c.CourseId,
                    c.CourseTitle,
                    c.CourseCode,
                    c.DurationHours,
                    c.TuitionFee,
                    CategoryName = c.Category.CategoryName,
                    InstructorName = c.Instructor.InstructorName
                })
                .ToList();

            dgCourses.ItemsSource = list;
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            int categoryId = cbFilterCategory.SelectedValue is int cId ? cId : 0;
            int instructorId = cbFilterInstructor.SelectedValue is int iId ? iId : 0;
            LoadCourses(categoryId, instructorId);
        }

        private void btnClearFilter_Click(object sender, RoutedEventArgs e)
        {
            cbFilterCategory.SelectedIndex = 0;
            cbFilterInstructor.SelectedIndex = 0;
            LoadCourses();
        }

        private void btnAddCourse_Click(object sender, RoutedEventArgs e)
        {
            string title = txtCourseTitle.Text.Trim();
            string code = txtCourseCode.Text.Trim();
            string durationText = txtDurationHours.Text.Trim();
            string feeText = txtTuitionFee.Text.Trim();

            if (string.IsNullOrWhiteSpace(title) ||
                string.IsNullOrWhiteSpace(code) ||
                string.IsNullOrWhiteSpace(durationText) ||
                string.IsNullOrWhiteSpace(feeText) ||
                cbAddCategory.SelectedValue == null ||
                cbAddInstructor.SelectedValue == null)
            {
                MessageBox.Show("Please fill in all fields and select both Category and Instructor.",
                    "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(durationText, out int durationHours) || durationHours <= 0)
            {
                MessageBox.Show("DurationHours must be a positive integer.",
                    "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!decimal.TryParse(feeText, out decimal tuitionFee) || tuitionFee < 0)
            {
                MessageBox.Show("TuitionFee must be a valid non-negative number.",
                    "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var course = new Course
            {
                CourseTitle = title,
                CourseCode = code,
                DurationHours = durationHours,
                TuitionFee = tuitionFee,
                CategoryId = (int)cbAddCategory.SelectedValue,
                InstructorId = (int)cbAddInstructor.SelectedValue
            };

            using var context = new Prn2122Context();
            context.Courses.Add(course);
            context.SaveChanges();

            int filterCategoryId = cbFilterCategory.SelectedValue is int cId ? cId : 0;
            int filterInstructorId = cbFilterInstructor.SelectedValue is int iId ? iId : 0;
            LoadCourses(filterCategoryId, filterInstructorId);

            ClearAddForm();
        }

        private void btnClearForm_Click(object sender, RoutedEventArgs e)
        {
            ClearAddForm();
        }

        private void ClearAddForm()
        {
            txtCourseTitle.Clear();
            txtCourseCode.Clear();
            txtDurationHours.Clear();
            txtTuitionFee.Clear();

            if (cbAddCategory.Items.Count > 0)
            {
                cbAddCategory.SelectedIndex = 0;
            }

            if (cbAddInstructor.Items.Count > 0)
            {
                cbAddInstructor.SelectedIndex = 0;
            }
        }
    }
}
