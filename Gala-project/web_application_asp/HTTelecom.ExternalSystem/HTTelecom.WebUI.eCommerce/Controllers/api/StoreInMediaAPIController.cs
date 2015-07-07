using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HTTelecom.Domain.Core.Repository.mss;
using HTTelecom.Domain.Core.DataContext.mss;

namespace HTTelecom.WebUI.eCommerce.Controllers.api
{
    public class StoreInMediaAPIController : ApiController
    {
        private StoreInMediaRepository storeInMediaRes = new StoreInMediaRepository();
        // GET api/<controller>
        public IEnumerable<StoreInMedia> Get(Boolean isHome)
        {
            //if(isHome)
            //    return storeInMediaRes.GetByHome();
            return storeInMediaRes.GetByHome().Distinct().OrderByDescending(n => n.Store.DateCreated).ToList();
        }

        // GET api/<controller>/5
        public IEnumerable<StoreInMedia> GetByStoreId(long id)
        {
            return storeInMediaRes.GetByStoreId(id);
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}