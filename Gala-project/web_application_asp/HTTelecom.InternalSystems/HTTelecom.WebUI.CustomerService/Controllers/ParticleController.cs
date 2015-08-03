using HTTelecom.Domain.Core.DataContext.mss;
using HTTelecom.Domain.Core.Repository.lps;
using HTTelecom.Domain.Core.Repository.mss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HTTelecom.WebUI.CustomerService.Controllers
{
    public class ParticleController : Controller
    {
        //
        // GET: /Particle/

        [HttpGet]
        public ActionResult GetShippingFee(long? ProvinceId, string lstProduct)//xử lí ajax
        {
            List<string> error = new List<string>();
            if (ProvinceId == null || lstProduct == "")
            {
                error.Add(" Province is null.");
                Json(new { success = false, shippingfee = "0 đ", error = error }, JsonRequestBehavior.AllowGet);
            }
            if (Request.IsAjaxRequest())
            {
                ProductRepository _ProductRepository = new ProductRepository();
                try
                {
                    string a = lstProduct;
                    dynamic data_lstProduct = Newtonsoft.Json.Linq.JObject.Parse(a);
                    List<Tuple<long, int>> lstProductId = new List<Tuple<long, int>>();
                    decimal totalPrice = 0;
                    foreach (var item in data_lstProduct.lstProduct)
                    {
                        long tmp_ProductId = 0;
                        int tmp_Quantity = 0;
                        int Quantity = int.TryParse(item.Quantity.ToString(), out tmp_Quantity) ? Convert.ToInt32(item.Quantity.ToString()) : 1;
                        if (long.TryParse(item.ProductId.ToString(), out tmp_ProductId))
                        {
                            var productItem = (Product)_ProductRepository.GetById(Convert.ToInt64(item.ProductId.ToString()));
                            double? price = productItem.PromotePrice != 0 ? productItem.PromotePrice : productItem.MobileOnlinePrice;
                            totalPrice += Convert.ToDecimal(price * Quantity);
                            lstProductId.Add(Tuple.Create(tmp_ProductId, Quantity));
                        }
                    }
                    ShipRepository _ShipRepository = new ShipRepository();
                    var ship = ProvinceId == null ? null : _ShipRepository.GetByTarget(Convert.ToInt64(ProvinceId), "2");
                    if (ship == null)
                        return Json(new
                        {
                            success = false,
                            shippingfee = string.Format("{0:0,0 đ}", 0),
                            error = "ship error"
                        }, JsonRequestBehavior.AllowGet);
                    if (totalPrice >= ship.FreeShip)
                        return Json(new
                        {
                            success = true,
                            //Giá công kềnh
                            shippingfee = 0,
                            shippingfee_write = string.Format("{0:0,0 đ}", 0),
                            //Giá Ship duy nhất
                            ship = ship.Price,
                            ship_write = string.Format("{0:0,0 đ}", ship.Price),
                            //Giá ShipFree duy nhất
                            shipfree = ship.FreeShip,
                            shipfree_write = string.Format("{0:0,0 đ}", ship.FreeShip),
                            message ="",
                            //Ship + Cồng kềnh
                            total_ship = 0,
                            total_ship_write = string.Format("{0:0,0 đ}", 0),
                            //Giá + Ship + Cồng kềnh
                            totalPrice = totalPrice,
                            totalPrice_write = string.Format("{0:0,0 đ}", totalPrice),
                            error = error
                        }, JsonRequestBehavior.AllowGet);
                    WeightRepository _WeightRepository = new WeightRepository();
                    decimal shippingfee = _WeightRepository.GetShippingFee((long)ProvinceId, lstProductId);
                    //Phí giao hàng: 10000 đ (Phí giao hàng: ,Phí cồng kenh : )
                    //ViewBag.multiRes.GetString("addtocart", ViewBag.CultureInfo)
                    //"Phí giao hàng:" + (shippingfee+ ship.Price)
                    var ship_write ="";
                    return Json(new
                    {
                        success = true,
                        //Giá công kềnh
                        shippingfee = shippingfee,
                        shippingfee_write = string.Format("{0:0,0 đ}", shippingfee),
                        //Giá ship duy nhất
                        ship = ship.Price,
                        ship_write = string.Format("{0:0,0 đ}", ship.Price),
                        //Giá ShipFree duy nhất
                        shipfree = ship.FreeShip,
                        shipfree_write = string.Format("{0:0,0 đ}", ship.FreeShip),
                        message ="",
                        // Ship + Cồng kềnh
                        total_ship = ship.Price + shippingfee,
                        total_ship_write = string.Format("{0:0,0 đ}", ship.Price + shippingfee),
                        //Giá + Ship + Cồng kềnh
                        totalPrice = totalPrice + shippingfee + ship.Price,
                        totalPrice_write = string.Format("{0:0,0 đ}", totalPrice + shippingfee + ship.Price),
                        error = error
                    }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    error.Add("Error: " + ex.Message);
                }
                return Json(new
                {
                    success = false,
                    shippingfee = string.Format("{0:0,0 đ}", 0),
                    error = error
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                success = false,
                shippingfee = string.Format("{0:0,0 đ}", 0),
                error = error
            }, JsonRequestBehavior.AllowGet);
        }

    }
}
