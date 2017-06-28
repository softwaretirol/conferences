using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DWXSample
{
    class Person
    {
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public Geschlecht Geschlecht { get; set; }

        public ICollection<Address> Adressen { get; set; }

    }

    class Address
    {
        public int Id { get; set; }

    }

    class PersonViewModel //: IDataErrorInfo
    {
        private Person person;

        public string Vorname
        {
            get => person.Vorname;
            set => person.Vorname = value;
        }

        public ObservableCollection<AddressViewModel> Addresses { get; set; }
        public IEnumerable<Geschlecht> AvailableGeschlechter
        {
            get { return Enum.GetValues(typeof(Geschlecht)).OfType<Geschlecht>(); }
        }
    }

    internal class AddressViewModel
    {
        private Address address;

        public int Id
        {
            get => address.Id;
        }

        public ICommand DeleteCommand { get; }

        private void Delete()
        {
            //
        }
    }

    enum Geschlecht
    {
        Männlich,
        Weiblich,
        Conchita
    }
}
