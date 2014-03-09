namespace Crm.Domain
{
    public class Tag
    {
        /// <summary>
        /// Instanciates a new tag
        /// </summary>
        public Tag()
        {
        }

        /// <summary>
        /// Re-Creates a new customer
        /// </summary>
        public Tag(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; private set; }

        public string Name { get; private set; }

        public void ChangeName(string name)
        {
            Name = name;
        }
    }
}
