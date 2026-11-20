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
            LoadMovies();
        }

        private void LoadComboBoxes()
        {
            using var context = new Prn2123Context();

            var genres = context.Genres.OrderBy(g => g.GenreId).ToList();
            var filterGenres = new List<Genre> { new Genre { GenreId = 0, GenreName = "All" } };
            filterGenres.AddRange(genres);

            cboFilterGenre.ItemsSource = filterGenres;
            cboFilterGenre.SelectedIndex = 0;

            cboAddGenre.ItemsSource = genres;
            if (genres.Count > 0)
            {
                cboAddGenre.SelectedIndex = 0;
            }

            var studios = context.Studios.OrderBy(s => s.StudioId).ToList();
            var filterStudios = new List<Studio> { new Studio { StudioId = 0, StudioName = "All" } };
            filterStudios.AddRange(studios);

            cboFilterStudio.ItemsSource = filterStudios;
            cboFilterStudio.SelectedIndex = 0;

            cboAddStudio.ItemsSource = studios;
            if (studios.Count > 0)
            {
                cboAddStudio.SelectedIndex = 0;
            }
        }

        private void LoadMovies()
        {
            using var context = new Prn2123Context();

            var query = context.Movies
                .Include(m => m.Genre)
                .Include(m => m.Studio)
                .AsQueryable();

            if (cboFilterGenre.SelectedValue is int genreId && genreId > 0)
            {
                query = query.Where(m => m.GenreId == genreId);
            }

            if (cboFilterStudio.SelectedValue is int studioId && studioId > 0)
            {
                query = query.Where(m => m.StudioId == studioId);
            }

            var movies = query
                .OrderBy(m => m.MovieId)
                .Select(m => new
                {
                    m.MovieId,
                    m.Title,
                    m.RatingCode,
                    m.ReleaseYear,
                    m.DurationMinutes,
                    GenreName = m.Genre.GenreName,
                    StudioName = m.Studio.StudioName
                })
                .ToList();

            dgMovies.ItemsSource = movies;
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            LoadMovies();
        }

        private void btnClearFilter_Click(object sender, RoutedEventArgs e)
        {
            cboFilterGenre.SelectedIndex = 0;
            cboFilterStudio.SelectedIndex = 0;
            LoadMovies();
        }

        private void btnAddMovie_Click(object sender, RoutedEventArgs e)
        {
            string title = txtTitle.Text.Trim();
            string ratingCode = txtRatingCode.Text.Trim();
            string releaseYearText = txtReleaseYear.Text.Trim();
            string durationText = txtDurationMinutes.Text.Trim();

            if (string.IsNullOrWhiteSpace(title) ||
                string.IsNullOrWhiteSpace(ratingCode) ||
                string.IsNullOrWhiteSpace(releaseYearText) ||
                string.IsNullOrWhiteSpace(durationText) ||
                cboAddGenre.SelectedValue == null ||
                cboAddStudio.SelectedValue == null)
            {
                MessageBox.Show("Please fill in all fields and select both Genre and Studio.",
                    "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(releaseYearText, out int releaseYear) || releaseYear <= 0)
            {
                MessageBox.Show("ReleaseYear must be a valid positive number.",
                    "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(durationText, out int durationMinutes) || durationMinutes <= 0)
            {
                MessageBox.Show("DurationMinutes must be a valid positive number.",
                    "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var newMovie = new Movie
            {
                Title = title,
                RatingCode = ratingCode,
                ReleaseYear = releaseYear,
                DurationMinutes = durationMinutes,
                GenreId = (int)cboAddGenre.SelectedValue,
                StudioId = (int)cboAddStudio.SelectedValue
            };

            using var context = new Prn2123Context();
            context.Movies.Add(newMovie);
            context.SaveChanges();

            LoadMovies();
            ClearAddForm();
        }

        private void btnClearForm_Click(object sender, RoutedEventArgs e)
        {
            ClearAddForm();
        }

        private void ClearAddForm()
        {
            txtTitle.Text = string.Empty;
            txtRatingCode.Text = string.Empty;
            txtReleaseYear.Text = string.Empty;
            txtDurationMinutes.Text = string.Empty;

            if (cboAddGenre.Items.Count > 0)
            {
                cboAddGenre.SelectedIndex = 0;
            }

            if (cboAddStudio.Items.Count > 0)
            {
                cboAddStudio.SelectedIndex = 0;
            }
        }
    }
}
