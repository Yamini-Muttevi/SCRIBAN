using Sitecore.Data.Fields;

namespace Sitecore.Foundation.SXAExtensions.Scriban.Model
{
    public class LinkInfo
    {
        public string url { get; set; }
        public string text { get; set; }
        public string title { get; set; }
        public string anchor { get; set; }
        public string target { get; set; }

        public LinkInfo() { }

        public LinkInfo(LinkField item)
        {
            if (item.LinkType == "internal")
            {
                url = item.GetFriendlyUrl();
                text = item.Text;
                title = item.Title;
                anchor = item.Anchor;
                target = item.Target;
            }
           if(item.LinkType == "external")
            {
                url = item.Url;
                text = item.Text;
                title = item.Title;
                anchor = item.Anchor;
                target = item.Target;
            }        
        }
    }
}
