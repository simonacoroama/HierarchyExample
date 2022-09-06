namespace HierarchyApi.Contracts
{
    public class Country
    {
        public string Name { get; set; }
        public string CountryCode { get; set; }

        public List<Chain> Children { get; set; }

    }
}