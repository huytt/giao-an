using HTTelecom.Domain.Core.DataContext.cis;
using HTTelecom.Domain.Core.IRepository.mss;
using HTTelecom.Domain.Core.Repository.cis;
using HTTelecom.Domain.Core.Repository.mss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace HTTelecom.WebUI.CustomerService.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Product/

        public ActionResult Search(string keyword)
        {
            ISearchRepository _searchRep = new SearchRepository();
            var rs = _searchRep.SearchProductSimple(keyword, -1);
            if (rs == null)
            {
                return null;
            }
            List<string> lst = new List<string>();
            List<string> lstName = new List<string>();
            List<string> lstKeyword = new List<string>();
            foreach (var item in rs)
            {
                if (item.ProductStockCode.ToUpper().IndexOf(keyword.ToUpper()) >= 0)
                {
                    lstName.Add(item.ProductStockCode);
                }
                if (item.ProductName != null)
                {
                    var rs_name = "";
                    var name = item.ProductName.ToLower().IndexOf(keyword.ToLower());
                    if (name >= 0)
                    {
                        var tempbytemp = name + keyword.Length;
                        rs_name = item.ProductName.Substring(name, keyword.Length);
                        if (item.ProductName.Length >= tempbytemp)
                            if (item.ProductName[tempbytemp] == ' ')
                            {
                                lstName.Add(rs_name.ToUpper());
                                rs_name += " ";
                                tempbytemp++;
                            }
                        for (int i = tempbytemp; i < item.ProductName.Length; i++)
                        {
                            if (item.ProductName[i] != ' ' && item.ProductName[i] != ',' && item.ProductName[i] != '.')
                            {
                                rs_name += item.ProductName[i];
                            }
                            else
                                break;
                        }
                        lstName.Add(rs_name.ToUpper());
                    }
                }

                if (item.Keywords != null)
                {
                    var rs_keyword = item.Keywords.Split(',').ToList();
                    if (rs_keyword.Count > 0)
                    {
                        foreach (var itemkey in rs_keyword)
                        {
                            var keyIndex = itemkey.ToLower().IndexOf(keyword.ToLower());
                            if (keyIndex >= 0)
                            {
                                lstKeyword.Add(itemkey.Trim().ToUpper());
                            }
                        }
                    }
                }
            }
            lst.AddRange(lstName);
            lst.AddRange(lstKeyword);
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(lst.Distinct());
            return Json(json);
        }
        public ActionResult GetCustomer(string CustomerCode, string CustomerEmail)
        {
            try
            {
                CustomerRepository _CustomerRepository = new CustomerRepository();
                if (CustomerCode != null && CustomerCode.Length > 0)
                {
                    Customer _cus = _CustomerRepository.GetByCode(CustomerCode);
                    if (_cus != null)
                        return Json("{\"CustomerId\":\"" + _cus.CustomerId + "\",\"CustomerName\":\"" + _cus.FirstName + " " + _cus.LastName + "\",\"CustomerPhone\":\"" + _cus.Phone + "\",\"CustomerEmail\":\"" + _cus.Email + "\"}");
                }
                else
                {
                    Customer _cus = _CustomerRepository.GetByEmail(CustomerEmail);
                    if (_cus != null)
                        return Json("{\"CustomerId\":\"" + _cus.CustomerId + "\",\"CustomerName\":\"" + _cus.FirstName + " " + _cus.LastName + "\",\"CustomerPhone\":\"" + _cus.Phone + "\",\"CustomerEmail\":\"" + _cus.Email + "\"}");
                }
                return Json("");
            }
            catch
            {
                return Json("");
            }
        }
        public ActionResult GetProductPage(string keyword)
        {
            try
            {
                var toDay = DateTime.Now;
                ISearchRepository _searchRep = new SearchRepository();
                var raw = "[";
                var rs = _searchRep.SearchProductSimple(keyword, -1).Distinct().ToList();
                foreach (var item in rs)
                {

                    if (item.IsDeleted == false && item.IsVerified == true && item.Store.IsDeleted == false && item.Store.IsVerified == true && item.Store.OnlineDate != null && item.Store.OfflineDate != null && (toDay - item.Store.OnlineDate.Value).TotalMinutes >= 0 && (item.Store.OfflineDate.Value - toDay).TotalMinutes >= 0)
                    {
                        raw += "{\"ProductId\":\"" + item.ProductId + "\",\"ProductStockId\":\"" + item.ProductStockCode + "\",\"Price\":\"" + (item.PromotePrice != null ? item.PromotePrice.ToString() : item.MobileOnlinePrice.ToString()) + "\",\"OnlinePrice\":\"" + item.MobileOnlinePrice.ToString() + "\",\"ProductName\":\"" + ReplaceAll(item.ProductName) + "\"},";
                    }
                }
                if (raw == "[")
                    raw = "[]";
                else
                    raw = raw.Substring(0, raw.Length - 1) + "]";
                return Json(raw);
            }
            catch
            {
                return Json("[]");
            }
        }
        public string ReplaceAll(string input)
        {
            var s = input.IndexOf("'");
            if (input.IndexOf("'") >= 0 || input.IndexOf('"') >= 0)
            {
                input = input.Replace("'", HttpUtility.HtmlEncode("'"));
                input = input.Replace('"', ' ');
                return ReplaceAll(input);
            }
            else
                return input;
        }

    }
}
