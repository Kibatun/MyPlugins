using System.ComponentModel;
using System;

namespace MyPlugins
{
    public class CategoryCountViewModel : IComparable<CategoryCountViewModel>, INotifyPropertyChanged
    {
        private string _category;
        private int _count;

        public string Category
        {
            get { return _category; }
            set
            {
                _category = value;
                OnPropertyChanged(nameof(Category));
            }
        }

        public int Count
        {
            get { return _count; }
            set
            {
                _count = value;
                OnPropertyChanged(nameof(Count));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int CompareTo(CategoryCountViewModel other)
        {
            // Сравнение по алфавиту
            return string.Compare(Category, other?.Category, StringComparison.OrdinalIgnoreCase);
        }
    }
}
