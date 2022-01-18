using Scriban.Runtime;
using Sitecore.XA.Foundation.Abstractions;
using Sitecore.XA.Foundation.Scriban.Pipelines.GenerateScribanContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sitecore.Data.Items;
using Sitecore.Data.Fields;
using Sitecore.Mvc.Presentation;

namespace Sitecore.Foundation.SXAExtensions.Scriban.Pipelines.GenerateScribanContext
{
    public class GetURLCompare : IGenerateScribanContextProcessor
    {
        private readonly IContext context;
        protected readonly IPageMode PageMode;
        private delegate Boolean Delegate(Item item, String URL);

        public GetURLCompare(IPageMode pageMode, IContext context)
        {
            PageMode = pageMode;
            this.context = context;
        }

        public void Process(GenerateScribanContextPipelineArgs args)
        {
            args.GlobalScriptObject.Import("sc_url_compare", new Delegate(GetURLCompareValue));
        }

        public Boolean GetURLCompareValue(Item item, string URL)
        {


            if (item != null && URL!=null)
            {
             var itmURL = Sitecore.Links.LinkManager.GetItemUrl(Sitecore.Context.Item);
              var lnkURL = URL;

                if (itmURL.Contains(lnkURL) == true)
                {
                    return true;
                }
                return false;
            }

            return false;

            

        }
    }
}
