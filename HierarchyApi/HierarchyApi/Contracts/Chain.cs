namespace HierarchyApi.Contracts
{
    public class Chain
    {
        public string Name { get; set; }

        public Attribute Attributes { get; set; }

        public List<Store> Children { get; set; }
    }
}