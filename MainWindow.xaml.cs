using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace FilterApp
{
    public partial class MainWindow : Window
    {
        // Делегат для фильтрации
        public delegate List<string> FilterDelegate(List<string> data);

        private List<string> dataList;

        public MainWindow()
        {
            InitializeComponent();
            // Инициализируем данные
            dataList = new List<string>
            {
                "2023-01-01: Новый год",
                "2023-03-08: Международный женский день",
                "2024-11-25: 18 лет Ане будет",
                "2024-12-25: Рождество",
                "2024-01-01: Новый год"
            }; 
        }

        // Фильтрация по ключевым словам
        public List<string> FilterByKeyword(List<string> data)
        {
            string keyword = KeywordTextBox.Text.ToLower();
            return data.Where(item => item.ToLower().Contains(keyword)).ToList();
        }

        // Фильтрация по дате
        public List<string> FilterByDate(List<string> data)
        {
            if (DateTime.TryParse(DateTextBox.Text, out DateTime filterDate))
            {
                return data.Where(item => item.StartsWith(filterDate.ToString("yyyy-MM-dd"))).ToList();
            }
            else
            {
                ResultTextBlock.Text = "Неверный формат даты. Введите в формате YYYY-MM-DD.";
                return new List<string>();
            }
        }

        // Обработчик нажатия на кнопку "Применить фильтр"
        private void ApplyFilterButton_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem selectedFilter = (ComboBoxItem)FilterComboBox.SelectedItem;

            if (selectedFilter == null)
            {
                ResultTextBlock.Text = "Выберите фильтр для применения.";
                return;
            }

            FilterDelegate filterDelegate;

            // Применяем фильтр в зависимости от выбора пользователя
            if (selectedFilter.Content.ToString() == "Фильтр по ключевым словам")
            {
                filterDelegate = FilterByKeyword;
            }
            else if (selectedFilter.Content.ToString() == "Фильтр по дате")
            {
                filterDelegate = FilterByDate;
            }
            else
            {
                ResultTextBlock.Text = "Ошибка фильтрации.";
                return;
            }

            // Применение выбранного фильтра
            List<string> filteredData = filterDelegate(dataList);

            // Выводим результат
            if (filteredData.Any())
            {
                ResultTextBlock.Text = "Отфильтрованные данные:\n" + string.Join("\n", filteredData);
            }
            else
            {
                ResultTextBlock.Text = "Нет совпадений с фильтром.";
            }
        }
    }
}
