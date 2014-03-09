using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EfTag = Crm.Infrastructure.Tag;

namespace Crm.Infrastructure
{
    public class TagRepository
    {
        private readonly CrmEntities ctx;

        public TagRepository(CrmEntities ctx)
        {
            this.ctx = ctx;
        }

        public Domain.Tag Get(int id)
        {
            var mapping = BuildQuery(t => t.Id == id);
            return mapping.First();
        }

        public IList<Domain.Tag> GetAll()
        {
            var mapping = BuildQuery(t => true);
            return mapping.ToList();
        }

        public int Create(Domain.Tag tag)
        {
            var newTag = new EfTag { Name = tag.Name };

            ctx.Tags.Add(newTag);
            ctx.SaveChanges();

            return newTag.Id;
        }

        private IEnumerable<Domain.Tag> BuildQuery(Expression<Func<Tag, bool>> where)
        {
            return ctx.Tags
                .Where(where)
                .ToList()
                .Select(c => 
                    new Domain.Tag(
                        c.Id,
                        c.Name
                    )
                );
        }
    }
}
