namespace HierarchyApi.Contracts
{
    public class Store
    {
        public string Name { get; set; }

        public int Number { get; set; }

        public List<Workstation> Children { get; set; }
    }
}