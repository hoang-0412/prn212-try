using Microsoft.EntityFrameworkCore;
using Q2.Models;
using System.Windows;
using System.Windows.Controls;

namespace Q2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadBooks();
        }

        private void LoadBooks(string? title = null, string? publisher = null)
        {
            using var context = new PePrn26sumB5Context();
            var query = context.Books.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(title))
            {
                query = query.Where(b => b.Title.Contains(title));
            }

            if (!string.IsNullOrWhiteSpace(publisher))
            {
                query = query.Where(b => b.Publisher != null && b.Publisher.Contains(publisher));
            }

            dgBooks.ItemsSource = query.ToList();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            LoadBooks(txtSearchTitle.Text.Trim(), txtSearchPublisher.Text.Trim());
        }

        private void dgBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgBooks.SelectedItem is Book selectedBook)
            {
                txtBookId.Text = selectedBook.BookId.ToString();
                txtTitle.Text = selectedBook.Title;
                txtPublisher.Text = selectedBook.Publisher ?? string.Empty;
                txtPublicationYear.Text = selectedBook.PublicationYear?.ToString() ?? string.Empty;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtBookId.Text, out int bookId))
            {
                MessageBox.Show("Please select a book to edit.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string title = txtTitle.Text.Trim();
            string publisher = txtPublisher.Text.Trim();
            string yearText = txtPublicationYear.Text.Trim();

            if (string.IsNullOrWhiteSpace(title) ||
                string.IsNullOrWhiteSpace(publisher) ||
                string.IsNullOrWhiteSpace(yearText))
            {
                MessageBox.Show("Title, Publisher, and Publication Year cannot be empty.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(yearText, out int publicationYear))
            {
                MessageBox.Show("Publication Year must be a valid integer.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using var context = new PePrn26sumB5Context();
            var book = context.Books.Find(bookId);
            if (book == null)
            {
                MessageBox.Show("Book not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            book.Title = title;
            book.Publisher = publisher;
            book.PublicationYear = publicationYear;

            context.SaveChanges();

            LoadBooks(txtSearchTitle.Text.Trim(), txtSearchPublisher.Text.Trim());
            MessageBox.Show("Book updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
