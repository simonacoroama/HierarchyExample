namespace HierarchyApi.Contracts
{
    public class Chain
    {
        public int Number { get; set; }

        public string Name { get; set; }

        public Guid Id { get; set; }

        public List<Store> Children { get; set; }
    }
}