using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace MyPlugins
{
    public class ElementCountViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<CategoryCountViewModel> _categoryCounts;
        private readonly UIDocument _uidoc;

        public ElementCountViewModel(UIDocument uidoc)
        {
            _uidoc = uidoc;
            CountElements();
        }

        public ObservableCollection<CategoryCountViewModel> CategoryCounts
        {
            get => _categoryCounts;
            set
            {
                _categoryCounts = value;
                OnPropertyChanged(nameof(CategoryCounts));
            }
        }

        private void CountElements()
        {
            var doc = _uidoc.Document;
            var allElements = new FilteredElementCollector(doc)
                .WhereElementIsNotElementType()
                .ToElements();

            var categoryCounts = CountElementsByCategory(allElements);

            CategoryCounts = new ObservableCollection<CategoryCountViewModel>(
                categoryCounts.Select(x => new CategoryCountViewModel
                {
                    Category = x.Key,
                    Count = x.Value
                }).OrderBy(x => x)); // Сортировка по алфавиту
        }

        private Dictionary<string, int> CountElementsByCategory(ICollection<Element> elements)
        {
            var categoryCounts = new Dictionary<string, int>();

            foreach (var element in elements)
            {
                var categoryName = element.Category?.Name;

                if (!string.IsNullOrEmpty(categoryName))
                {
                    categoryCounts.TryGetValue(categoryName, out var count);
                    categoryCounts[categoryName] = count + 1;
                }
            }

            return categoryCounts;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
