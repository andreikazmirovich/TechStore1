namespace TechStore
{
    public class TechnicalDetail
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public TechnicalDetail(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}