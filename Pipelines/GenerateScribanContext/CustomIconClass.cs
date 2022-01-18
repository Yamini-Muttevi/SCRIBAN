using System;
using Scriban.Runtime;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.XA.Foundation.Abstractions;
using Sitecore.XA.Foundation.Scriban.Pipelines.GenerateScribanContext;

namespace Sitecore.Foundation.SXAExtensions.Scriban.Pipelines.GenerateScribanContext
{
    public class CustomIconClass : IGenerateScribanContextProcessor
    {
        private readonly IContext context;
        private delegate string CustomIconClassDelegate(Item item, string CustomIconField);

        public CustomIconClass(IContext context)
        {
            this.context = context;
        }
        public void Process(GenerateScribanContextPipelineArgs args)
        {
            var customIconClass = new CustomIconClassDelegate(GetCustomIconClass);
            args.GlobalScriptObject.Import("sc_customIconClass", (Delegate)customIconClass);
        }
        public string GetCustomIconClass(Item item, string CustomIconField)
        {
            Assert.ArgumentNotNull((object)item, nameof(item));
            Assert.ArgumentNotNull((object)CustomIconField, nameof(CustomIconField));


            if (item != null && CustomIconField != null)
            {
                if (Sitecore.Context.Database.GetItem(item.Fields[CustomIconField].Value) != null)
                {
                    string cssClass = String.Empty;
                    cssClass = Sitecore.Context.Database.GetItem(item.Fields[CustomIconField].Value).Fields["CssClass"].Value;
                    return cssClass;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}