using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyPlugins
{
    [Transaction(TransactionMode.Manual)]
    public class ElementCountPlugin : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                UIDocument uidoc = commandData.Application.ActiveUIDocument;
                Document doc = uidoc.Document;

                FilteredElementCollector collector = new FilteredElementCollector(doc);
                ICollection<Element> allElements = collector.WhereElementIsNotElementType().ToElements();

                var categoryCounts = CountElementsByCategory(allElements);

                ShowResultDialog(categoryCounts);

                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Result.Failed;
            }
        }

        private Dictionary<string, int> CountElementsByCategory(ICollection<Element> elements)
        {
            var categoryCounts = new Dictionary<string, int>();

            foreach (var element in elements)
            {
                var categoryName = element.Category?.Name; // Используем ?. для избежания NullReferenceException

                if (!string.IsNullOrEmpty(categoryName))
                {
                    if (categoryCounts.ContainsKey(categoryName))
                        categoryCounts[categoryName]++;
                    else
                        categoryCounts.Add(categoryName, 1);
                }
            }

            return categoryCounts;
        }

        private void ShowResultDialog(Dictionary<string, int> categoryCounts)
        {
            var categoryCountViewModels = new ObservableCollection<CategoryCountViewModel>();

            foreach (var categoryCount in categoryCounts)
            {
                categoryCountViewModels.Add(new CategoryCountViewModel
                {
                    Category = categoryCount.Key,
                    Count = categoryCount.Value
                });
            }

            CountResultsWindow resultsWindow = new CountResultsWindow(categoryCountViewModels);
            resultsWindow.ShowDialog();
        }
    }
}
