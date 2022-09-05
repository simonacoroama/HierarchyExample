namespace HierarchyApi
{
    public class Store
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Number { get; set; }

        public List<Workstation> Children { get; set; }
    }
}