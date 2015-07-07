using HTTelecom.Domain.Core.DataContext.acs;
using HTTelecom.Domain.Core.Repository.acs;
using Revalee.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace HTTelecom.WebUI.SaleMedia.Controllers
{
    public class RssController : Controller
    {
        string host = "www.galagala.vn/Medias/Images/";
        //
        // GET: /Rss/
        //public ActionResult Auto()
        //{
        //    //var serviceHost = "http://localhost:7611";
        //    //var callbackDelay = TimeSpan.FromHours(1.0);
        //    //var callbackUri = new Uri(serviceHost+Url.Action("CreateRss", "Rss"));
        //    //RevaleeRegistrar.ScheduleCallback("http://localhost:7611",callbackDelay, callbackUri);

        //    DateTimeOffset callbackTime = DateTimeOffset.Now.AddHours(12.0);

        //    // The callback should be in 12 hours from now
        //    Uri callbackUrl = new Uri(string.Format("http://localhost:7611" + Url.Action("CreateRss", "Rss")));

        //    // Register the callback request with the Revalee service
        //    RevaleeRegistrar.ScheduleCallback("http://localhost:7611", callbackTime, callbackUrl);
        //    CreateRss();
        //    return new EmptyResult();
        //}
        private AdsRepository _AdsRepository; 
        public RssController()
        {
            _AdsRepository = new AdsRepository();
        }
        public ActionResult CreateRss()
        {
            var lst = _AdsRepository.GetList_AdsAll(false, true);
            CreateList(lst);
            return new RedirectResult("http://localhost:7611/Rss.xml");
        }
        private void CreateList(List<Ad> lstRss)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("../Rss.xml"));
            XmlNode NoteTree = doc.DocumentElement;
            XmlNode NoteGoc = NoteTree.ChildNodes[0].ChildNodes[5];
            NoteGoc.RemoveAll();
            foreach (var itemRss in lstRss)
            {
                XmlNode item = doc.CreateElement("content");
                XmlNode header = doc.CreateElement("header");
                header.AppendChild(doc.CreateTextNode(itemRss.AdsContent.AdsHeader));
                item.AppendChild(header);
                XmlNode title = doc.CreateElement("title");
                title.AppendChild(doc.CreateTextNode(itemRss.AdsContent.AdsTitle));
                item.AppendChild(title);
                XmlNode CardId = doc.CreateElement("cardId");
                CardId.AppendChild(doc.CreateTextNode(itemRss.AdsId.ToString()));
                item.AppendChild(CardId);
                XmlNode ChannelId = doc.CreateElement("channelId");
                ChannelId.AppendChild(doc.CreateTextNode("None"));
                item.AppendChild(ChannelId);
                XmlNode Content = doc.CreateElement("content");
                Content.AppendChild(doc.CreateTextNode(itemRss.AdsContent.Description));
                item.AppendChild(Content);
                XmlNode Logo = doc.CreateElement("logo");
                Logo.AppendChild(doc.CreateTextNode(host+itemRss.AdsContent.LogoFilePath));
                item.AppendChild(Logo);
                XmlNode Media = doc.CreateElement("media");
                if (itemRss.AdsContent.ImageFilePath != null || itemRss.AdsContent.ImageFilePath.Length>0)
                    Media.AppendChild(doc.CreateTextNode(host+itemRss.AdsContent.ImageFilePath));
                else
                    Media.AppendChild(doc.CreateTextNode(itemRss.AdsContent.VideoFilePath));
                item.AppendChild(Media);
                XmlNode OnlineDate = doc.CreateElement("onlineDate");
                OnlineDate.AppendChild(doc.CreateTextNode(itemRss.OnlineDate.Value.ToString()));
                item.AppendChild(OnlineDate);
                XmlNode OnlinePeriod = doc.CreateElement("onlinePeriod");
                OnlinePeriod.AppendChild(doc.CreateTextNode(itemRss.PeriodOnline.Value.ToString()));
                item.AppendChild(OnlinePeriod);
                XmlNode Status = doc.CreateElement("status");
                if (itemRss.IsActive != null && itemRss.IsActive == true)
                Status.AppendChild(doc.CreateTextNode("online"));
                else Status.AppendChild(doc.CreateTextNode("offline"));
                item.AppendChild(Status);
                XmlNode link = doc.CreateElement("link");
                link.AppendChild(doc.CreateTextNode(itemRss.AdsContent.LinkSite));
                item.AppendChild(link);
                NoteGoc.AppendChild(item);
            }
            NoteTree.ChildNodes[0].ChildNodes[4].InnerXml = (DateTime.Now.ToString());
            doc.Save(Server.MapPath("../Rss.xml"));
        }
    }
}
