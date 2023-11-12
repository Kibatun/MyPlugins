using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyPlugins
{
    /// <summary>
    /// Логика взаимодействия для CountResultsWindow.xaml
    /// </summary>
    public partial class CountResultsWindow : Window
    {
        public ObservableCollection<CategoryCountViewModel> CategoryCounts { get; set; }

        public CountResultsWindow(ObservableCollection<CategoryCountViewModel> categoryCounts)
        {
            InitializeComponent();
            CategoryCounts = categoryCounts;
            CategoryDataGrid.ItemsSource = CategoryCounts;
        }
    }
}
