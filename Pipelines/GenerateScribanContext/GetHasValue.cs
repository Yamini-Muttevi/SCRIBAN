using Sitecore.XA.Foundation.Abstractions;
using Sitecore.XA.Foundation.Scriban.Pipelines.GenerateScribanContext;
using System;
using Sitecore.Data.Items;
using Scriban.Runtime;

namespace Sitecore.Foundation.SXAExtensions.Scriban.Pipelines.GenerateScribanContext
{
    public class GetHasValue : IGenerateScribanContextProcessor
    {
        private readonly IContext context;
        private delegate Boolean Delegate(Item item, string fieldName);

        public GetHasValue(IContext context)
        {
            this.context = context;
        }

        public void Process(GenerateScribanContextPipelineArgs args)
        {
            args.GlobalScriptObject.Import("sc_has_value", new Delegate(GetHasValueImpl));
        }

        public Boolean GetHasValueImpl(Item item, string fieldName)
        {
            if (item != null && fieldName != null)
            {
                //Change for fixing the case where value comes from Standard values https://stackoverflow.com/questions/35647588/sitecores-field-hasvalue-returning-false-even-when-there-is-a-value
                if (item.Fields[fieldName].Value != string.Empty && item.Fields[fieldName].Value != null && (item.Fields[fieldName].HasValue == true || item.Fields[fieldName].ContainsStandardValue == true))
                {
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}
