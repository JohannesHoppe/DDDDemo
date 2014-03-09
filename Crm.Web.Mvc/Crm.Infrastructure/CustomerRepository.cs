using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Crm.Domain;
using EfCustomer = Crm.Infrastructure.Customer;

namespace Crm.Infrastructure
{
    public class CustomerRepository
    {
        private readonly CrmEntities ctx;

        public CustomerRepository(CrmEntities ctx)
        {
            this.ctx = ctx;
        }

        public Domain.Customer Get(int id)
        {
            var mapping = BuildQuery(c => c.Id == id);
            return mapping.First();
        }

        public IList<Domain.Customer> GetAll()
        {
            var mapping = BuildQuery(c => true);
            return mapping.ToList();
        }

        public int Create(Domain.Customer customer)
        {
            var newCustomer = new EfCustomer {Name = customer.Name};

            ctx.Customers.Add(newCustomer);
            ctx.SaveChanges();

            if (customer.AssignedTags.Any())
            {
                foreach (AssignedTag assignedTag in customer.AssignedTags)
                {
                    var customerToTag = new CustomerToTag
                                            {
                                                TagId = assignedTag.Tag.Id,
                                                Created = assignedTag.Created
                                            };

                    newCustomer.CustomerToTags.Add(customerToTag);

                }

                ctx.SaveChanges();
            }

            return newCustomer.Id;
        }

        private IEnumerable<Domain.Customer> BuildQuery(Expression<Func<Customer, bool>> where)
        {
            return ctx.Customers
                .Include("CustomerToTags.Tag")
                .Where(where)
                .ToList()
                .Select(c =>
                    new Domain.Customer(
                        c.Id,
                        c.Name,
                        c.CustomerToTags.Select(t =>
                            new AssignedTag(
                                new Domain.Tag(t.TagId, t.Tag.Name),
                                t.Created
                            )
                        )
                    )
                );
        }
    }
}
