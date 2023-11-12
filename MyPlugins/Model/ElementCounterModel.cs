using Autodesk.Revit.DB;
using System.Collections.Generic;

public class ElementCounterModel
{
    public Dictionary<string, int> CountElementsByCategory(ICollection<Element> elements)
    {
        var categoryCounts = new Dictionary<string, int>();

        foreach (var element in elements)
        {
            var categoryName = element.Category?.Name;

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
}