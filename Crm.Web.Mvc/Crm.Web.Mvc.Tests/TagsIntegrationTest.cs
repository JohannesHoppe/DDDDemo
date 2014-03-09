using System.Linq;
using Crm.Infrastructure;
using Machine.Specifications;
using FluentAssertions;

namespace Crm.Web.Mvc.IntegrationTests
{
    [Subject("IntegrationTest")]
    public class when_create_two_tags_and_one_customer
    {
        static CrmEntities ctx;
        static CustomerRepository customerRepository ;
        static TagRepository tagRepository ;

        Establish context = () =>
        {
            ctx = new CrmEntities();

            Setup.ResetDatabase(ctx);

            customerRepository = new CustomerRepository(ctx);
            tagRepository = new TagRepository(ctx);
        };

        Because of = () =>
        {
            var newTag1 = new Domain.Tag();
            newTag1.ChangeName("Dummy Tag 1");

            var newTag2 = new Domain.Tag();
            newTag2.ChangeName("Dummy Tag 2");

            int newTag1Id = tagRepository.Create(newTag1);
            int newTag2Id = tagRepository.Create(newTag2);

            var persistendTag1 = tagRepository.Get(newTag1Id);
            var persistendTag2 = tagRepository.Get(newTag2Id);

            var newCustomer = new Domain.Customer();
            newCustomer.ChangeName("Dummy Customer");

            newCustomer.AssignTag(persistendTag1);
            newCustomer.AssignTag(persistendTag2);

           int newCustomerId = customerRepository.Create(newCustomer);

            // and just to be sure: load the customer again

            customerRepository.Get(newCustomerId);

        };

        It should_store_one_customer = () => ctx.Customers.Count().Should().Be(1);
        It should_store_two_tags = () => ctx.Tags.Count().Should().Be(2);
        It should_use_two_relation_table_entries = () => ctx.CustomerToTags.Count().Should().Be(2);

        Cleanup stuff = () =>
                            {
                                Setup.ResetDatabase(ctx);
                                ctx.Dispose();
                            };
    }
}
