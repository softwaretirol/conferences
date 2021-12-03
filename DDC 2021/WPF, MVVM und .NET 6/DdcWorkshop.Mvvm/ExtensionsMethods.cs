using System.Collections.ObjectModel;

namespace DdcWorkshop.Mvvm;

public static class ExtensionsMethods
{
    public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> itemsToAdd)
    {
        foreach (var item in itemsToAdd)
        {
            collection.Add(item);
        }
    }
}