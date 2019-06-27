namespace DwxWorkshop.AppFx
{
    public static class DefaultPropertyValidationRules
    {
        public static Property<string> HasMaxLength(this Property<string> property, int maxLength)
        {
            return property.AddValidationRule(x => x?.Length < maxLength ? string.Empty : "Zu lang.");
        }
        public static Property<string> HasMinLength(this Property<string> property, int mminLength)
        {
            return property.AddValidationRule(x => x?.Length > mminLength ? string.Empty : "Zu kurz.");
        }
    }
}