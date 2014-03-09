using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

// ReSharper disable UnusedParameter.Local
namespace Crm.Domain
{
    public class Customer
    {
        private readonly List<AssignedTag> assignedTags;

        /// <summary>
        /// Instanciates a new customer
        /// </summary>
        public Customer()
        {
            assignedTags = new List<AssignedTag>();
        }

        /// <summary>
        /// Re-Creates a new customer
        /// </summary>
        public Customer(int id, string name, IEnumerable<AssignedTag> existingAssignedTags)
        {
            CheckName(name);

            Id = id;
            Name = name;
            assignedTags = new List<AssignedTag>(existingAssignedTags);
        }

        public int Id { get; private set; }

        public string Name { get; private set; }

        public ReadOnlyCollection<AssignedTag> AssignedTags
        {
            get
            {
                return new ReadOnlyCollection<AssignedTag>(assignedTags);
            }
        }

        public void ChangeName(string name)
        {
            CheckName(name);
            Name = name;
        }

        public void AssignTag(Tag tag)
        {
            var assignedTag = new AssignedTag(tag, DateTime.Now);
            assignedTags.Add(assignedTag);
        }

        private void CheckName(string name)
        {
            if (name == "Highlander")
            {
                throw new ArgumentException("There can be only one Highlander!"); 
            }
        }

    }
}
// ReSharper restore UnusedParameter.Local
