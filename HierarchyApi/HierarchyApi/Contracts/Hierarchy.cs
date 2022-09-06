namespace HierarchyApi.Contracts
{
    public class Hierarchy
    {
        public string Name { get; set; }

        public List<Country> Children { get; set; }
    }
}
