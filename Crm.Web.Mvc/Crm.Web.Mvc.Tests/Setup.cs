using Crm.Infrastructure;

namespace Crm.Web.Mvc.IntegrationTests
{
    public class Setup
    {
        public static void ResetDatabase(CrmEntities context) 
        {
            context.CustomerToTags.RemoveRange(context.CustomerToTags);
            context.Tags.RemoveRange(context.Tags);
            context.Customers.RemoveRange(context.Customers);

            context.SaveChanges();
        }
    }
}
