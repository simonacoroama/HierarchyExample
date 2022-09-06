namespace HierarchyApi.Contracts
{
    public class Country
    {
        public string Name { get; set; }

        public Attribute Attributes { get; set; }

        public List<Chain> Children { get; set; }
    }
}