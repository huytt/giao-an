using HTTelecom.Domain.Core.DataContext.ams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.ams
{
    public class AuthenticationKeyRepository
    {
        public IList<AuthenticationKey> GetList_AuthenticationKeyAll()
        {
            using (AMS_DBEntities entities = new AMS_DBEntities())
            {
                return entities.AuthenticationKeys.ToList();
            }
        }

        public IList<AuthenticationKey> GetList_AuthenticationKeyAll_IsDeleted(bool isDeleted)
        {
            using (AMS_DBEntities entities = new AMS_DBEntities())
            {
                return entities.AuthenticationKeys.Where(a => a.IsDeleted == isDeleted).ToList();
            }
        }

        public IList<AuthenticationKey> GetList_AuthenticationKeyAll_IsActived(bool IsActived)
        {
            using (AMS_DBEntities entities = new AMS_DBEntities())
            {
                return entities.AuthenticationKeys.Where(a => a.IsActive == IsActived).ToList();
            }
        }

        public AuthenticationKey Get_AuthenticationKeyById(long authenticationKeyId)
        {
            using (AMS_DBEntities entities = new AMS_DBEntities())
            {
                return entities.AuthenticationKeys.Find(authenticationKeyId);
            }
        }

        public long Insert(AuthenticationKey authenticationKey)
        {
            using (AMS_DBEntities entities = new AMS_DBEntities())
            {
                try
                {
                    entities.AuthenticationKeys.Add(authenticationKey);
                    entities.SaveChanges();

                    return authenticationKey.AuthenticationKeyId;
                }
                catch
                {
                    throw new Exception("There was an error occurs or object already exists !!!");
                }

            }
        }

        public bool Update(AuthenticationKey authenticationKey)
        {
            using (AMS_DBEntities entities = new AMS_DBEntities())
            {
                try
                {
                    AuthenticationKey AuthenticationKeyToUpdate;
                    AuthenticationKeyToUpdate = entities.AuthenticationKeys.Where(x => x.AuthenticationKeyId == authenticationKey.AuthenticationKeyId).FirstOrDefault();
                    AuthenticationKeyToUpdate = authenticationKey;
                    //4. call SaveChanges
                    entities.SaveChanges();

                    return true;
                }
                catch
                {
                    throw new Exception("There was an error occurs or object already exists !!!");
                }
            }
        }
    }
}
