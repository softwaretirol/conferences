namespace DwxWorkshop.AppFx
{
    public class PersonViewModel2
    {
        public PersonViewModel2(Person person)
        {
            Vorname = new RefProperty<string>(() => person.Vorname, v => person.Vorname = v);
        }

        public RefProperty<string> Vorname { get; }
    }
}