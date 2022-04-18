namespace SecuredWebApp.Models
{
    public class IndexViewModel
    {
        public string Greeting { get; set; }
        public string Name { get; set; }

        public string FormattedGreeting
            => $"{Greeting}, {Name}!";
    }
}
