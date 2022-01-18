using Scriban.Runtime;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.XA.Foundation.Abstractions;
using Sitecore.XA.Foundation.Scriban.Pipelines.GenerateScribanContext;

namespace Sitecore.Foundation.SXAExtensions.Scriban.Pipelines.GenerateScribanContext
{
    public class GetDateFieldShortDate : IGenerateScribanContextProcessor
    {
        private readonly IContext context;
        private delegate string Delegate(Item item, string EventDate);

        public GetDateFieldShortDate(IContext context)
        {
            this.context = context;
        }

        public void Process(GenerateScribanContextPipelineArgs args)
        {
            args.GlobalScriptObject.Import("sc_date_short", new Delegate(GetShortDate));
        }

        public static string GetShortDate(Item item, string EventDate)
        {
            if (item?.Fields[EventDate] == null)
            {
                return string.Empty;
            }
            DateField field = item.Fields[EventDate];
            return field.DateTime.ToShortDateString();
        }
    }
}

