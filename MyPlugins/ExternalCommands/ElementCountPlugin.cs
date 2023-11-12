#region Usings
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
#endregion

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
                ElementCountViewModel viewModel = new ElementCountViewModel(uidoc);
                var categoryCounts = viewModel.CategoryCounts;
                CountResultsWindow resultsWindow = new CountResultsWindow(categoryCounts);
                resultsWindow.ShowDialog();

                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Result.Failed;
            }
        }
    }
}