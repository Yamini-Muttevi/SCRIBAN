using System;
using System.Globalization;
using Scriban.Runtime;
using Sitecore.XA.Foundation.Abstractions;
using Sitecore.XA.Foundation.Scriban.Pipelines.GenerateScribanContext;

namespace Sitecore.Foundation.SXAExtensions.Scriban.Pipelines.GenerateScribanContext
{
    public class GetDateCompare : IGenerateScribanContextProcessor
    {
        private readonly IContext context;
        private delegate string Delegate(string EventEyebrow,string EventDate);

        public GetDateCompare(IContext context)
        {
            this.context = context;
        }

        public void Process(GenerateScribanContextPipelineArgs args)
        {
            args.GlobalScriptObject.Import("sc_date_compare", new Delegate(GetDateCompareValue));
        }

        public string  GetDateCompareValue(string EventEyebrow,string EventDate)
        {
            if (EventEyebrow != null && EventDate != null )
            {
                string eyebrowDate = EventEyebrow;
                string eventDate = EventDate;
                if (DateTime.Parse(eyebrowDate, CultureInfo.InvariantCulture) > DateTime.Parse(eventDate, CultureInfo.InvariantCulture))
                {

                    string value = "Future Date";
                    return value;
                }
                if (DateTime.Parse(eyebrowDate, CultureInfo.InvariantCulture) == DateTime.Parse(eventDate, CultureInfo.InvariantCulture))
                {
                    string value = "Present date";
                    return value;
                }
                if (DateTime.Parse(eyebrowDate, CultureInfo.InvariantCulture) < DateTime.Parse(eventDate, CultureInfo.InvariantCulture))
                {
                    string value = "Past date";
                    return value;
                }
              }
            return null;
        }
    }
}



       
        

        

