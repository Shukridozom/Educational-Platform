namespace Project.Core.Domains
{
    public class Role
    {
        public Role()
        {
            Users = new List<User>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<User> Users { get; set; }
    }
}
