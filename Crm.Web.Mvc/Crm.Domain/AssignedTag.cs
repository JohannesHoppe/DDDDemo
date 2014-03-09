using System;

namespace Crm.Domain
{
    public class AssignedTag
    {
        public AssignedTag(Tag tag, DateTime created)
        {
            Tag = tag;
            Created = created;
        }

        public Tag Tag { get; private set; }

        public DateTime Created { get; private set; }
    }
}
