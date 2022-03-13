using Scriban.Runtime;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.XA.Foundation.Abstractions;
using Sitecore.XA.Foundation.Multisite.Services;
using Sitecore.XA.Foundation.Scriban.Pipelines.GenerateScribanContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Foundation.SXAExtensions.Scriban.Pipelines.GenerateScribanContext
{
    public class AddLinkFunction : IGenerateScribanContextProcessor
    {
        private readonly ILogService _logService;
        private readonly IContext _context;
        private readonly ILinkProviderService _linkProviderService;
        public AddLinkFunction(IContext context, ILinkProviderService linkProviderService, ILogService logService)
        {
            _logService = logService;
            _context = context;
            _linkProviderService = linkProviderService;
        }
        private delegate string GetLinkDelegate(Item item, string linkFieldName);
        private delegate bool IsExternal(Item item, string linkFieldName);
        public void Process(GenerateScribanContextPipelineArgs args)
        {
            args.GlobalScriptObject.Import("sc_link_geturl", new GetLinkDelegate(GetUrl));
            args.GlobalScriptObject.Import("sc_link_gettarget", new IsExternal(CheckTarget));
            args.GlobalScriptObject.Import("sc_link_getText", new GetLinkDelegate(GetText));

        }
        private string GetText(Item item, string generalLinkFieldName)
        {
            if (item?.Fields[generalLinkFieldName] == null)
                return string.Empty;

            try
            {
               
               
                    LinkField linkField = item.Fields[generalLinkFieldName];
                    return linkField == null ? string.Empty : linkField.Text;
               
            }
            catch (Exception e)
            {
                _logService.Error(e.Message, e, this);
                return string.Empty;
            }
        }
        private bool CheckTarget(Item item, string generalLinkFieldName)
        {
            if (item?.Fields[generalLinkFieldName] == null)
                return false;

            try
            {
               
                    LinkField linkField = item.Fields[generalLinkFieldName];
                    return linkField == null ? false : !linkField.IsInternal;
                
               
            }
            catch (Exception e)
            {
                _logService.Error(e.Message, e, this);
                return false;
            }
        }

        public string GetUrl(Item item, string generalLinkFieldName)
        {
            if (item?.Fields[generalLinkFieldName] == null)
                return "#";

            try
            {
               
               LinkField linkField = item.Fields[generalLinkFieldName];
                if (linkField == null)
                {
                    return "#";
                }
                else
                {
                    if (linkField.IsInternal)
                    {

                        return linkField.GetFriendlyUrl();
                    }
                    else
                    {

                        return linkField.Url;

                    }
                }
            }
            catch (Exception e)
            {
                _logService.Error(e.Message, e, this);
                return string.Empty;
            }
        }
    }
}