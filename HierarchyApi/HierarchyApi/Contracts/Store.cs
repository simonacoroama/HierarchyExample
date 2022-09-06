namespace HierarchyApi.Contracts
{
    public class Store
    {
        public string Name { get; set; }

        public Attribute Attributes { get; set; }

        public List<Workstation> Children { get; set; }
    }
}