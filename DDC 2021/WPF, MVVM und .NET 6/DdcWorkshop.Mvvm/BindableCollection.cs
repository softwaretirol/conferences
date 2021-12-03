using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace DdcWorkshop.Mvvm;

public class BindableCollection<T> : ObservableCollection<T>
{
    private bool suspendEvents;

    public BindableCollection()
    {
        
    }
    public BindableCollection(ICollection<T> items) : base(items)
    {
    }

    protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
        if (suspendEvents)
        {
            return;
        }

        base.OnCollectionChanged(e);
    }

    public void AddRange(IEnumerable<T> itemsToAdd)
    {
        suspendEvents = true;
        foreach (var item in itemsToAdd)
        {
            this.Add(item);
        }
        suspendEvents = false;

        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }
}