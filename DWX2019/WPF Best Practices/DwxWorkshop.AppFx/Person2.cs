namespace DwxWorkshop.AppFx
{
    public class Person2
    {
        public Person2()
        {
            Vorname = new Property<string>().HasMaxLength(100).HasMinLength(10);
        }

        public Property<string> Vorname { get; }
    }
}