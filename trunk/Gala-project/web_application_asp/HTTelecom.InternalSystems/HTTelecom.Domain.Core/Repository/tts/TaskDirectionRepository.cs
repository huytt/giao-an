using HTTelecom.Domain.Core.DataContext.tts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.Repository.tts
{
    public class TaskDirectionRepository
    {
        public IList<TaskDirection> GetAll()
        {
            using (TTS_DBEntities entities = new TTS_DBEntities())
            {
                return entities.TaskDirections.ToList();
            }
        }

        public IList<TaskDirection> GetList_IsDeleted(bool isDeleted)
        {
            using (TTS_DBEntities entities = new TTS_DBEntities())
            {
                return entities.TaskDirections.Where(a => a.IsDeleted == isDeleted).ToList();
            }
        }

        public TaskDirection GetById(long TaskDirectionId)
        {
            using (TTS_DBEntities entities = new TTS_DBEntities())
            {
                return entities.TaskDirections.Find(TaskDirectionId);
            }
        }

        public long Insert(TaskDirection TaskDirection)
        {
            using (TTS_DBEntities entities = new TTS_DBEntities())
            {
                try
                {
                    entities.TaskDirections.Add(TaskDirection);
                    entities.SaveChanges();
                    return TaskDirection.TaskDirectionId;
                }
                catch
                {
                    throw new Exception("There was an error occurs or object already exists !!!");
                }
            }
        }
        public long Create(TaskDirection TaskDirection)
        {
            try
            {
                TTS_DBEntities _data = new TTS_DBEntities();
                _data.TaskDirections.Add(TaskDirection);
                _data.SaveChanges();
                return TaskDirection.TaskDirectionId;
            }
            catch
            {
                return -1;
            }
        }
        public bool Update(TaskDirection taskDirection)
        {
            using (TTS_DBEntities entities = new TTS_DBEntities())
            {
                try
                {
                    var taskDirectionUpdate = this.GetById(taskDirection.TaskDirectionId);
                    if (taskDirectionUpdate != null)
                    {
                        taskDirectionUpdate = taskDirection;
                        entities.SaveChanges();
                    }

                    return true;
                }
                catch
                {
                    throw new Exception("There was an error occurs or object already exists !!!");
                }
            }
        }
        public TaskDirection GetByIsValidAndOrderQuere(string TaskFormCode, bool? IsValid, int OrderQueue)
        {
            try
            {
                TTS_DBEntities _data = new TTS_DBEntities();
                return _data.TaskDirections.Where(n => n.IsValid == null && n.OrderQueue == OrderQueue && n.TaskFormCode == TaskFormCode).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }
        public TaskDirection GetBy(string TaskFormCode, string StatusDirectionCode, int OrderQueue)
        {
            try
            {
                TTS_DBEntities _data = new TTS_DBEntities();
                return _data.TaskDirections.Where(n => n.StatusDirectionCode == StatusDirectionCode && n.OrderQueue == OrderQueue && n.TaskFormCode == TaskFormCode).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        public TaskDirection GetBy(string TaskFormCode, string StatusDirectionCode, bool isValid, int OrderQueue)
        {
            try
            {
                TTS_DBEntities _data = new TTS_DBEntities();
                return _data.TaskDirections.Where(n => n.StatusDirectionCode == StatusDirectionCode && n.OrderQueue == OrderQueue && n.IsValid == isValid && n.TaskFormCode == TaskFormCode).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }
        public TaskDirection GetBy(string TaskFormCode, string SystemCode, string StatusDirectionCode, bool isValid, int OrderQueue)
        {
            try
            {
                TTS_DBEntities _data = new TTS_DBEntities();
                return _data.TaskDirections.Where(n => n.StatusDirectionCode == StatusDirectionCode && n.SystemCode == SystemCode && n.TaskFormCode == TaskFormCode && n.OrderQueue == OrderQueue && n.IsValid == isValid && n.TaskFormCode == TaskFormCode).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }
        public int GetOrderQueueByDepartment(string Department)
        {
            try
            {
                TTS_DBEntities _data = new TTS_DBEntities();
                var lst = _data.TaskDirections.Where(n => n.SystemCode == Department).ToList();
                if (lst.Count > 0)
                    return Convert.ToInt32(lst[0].OrderQueue);
                else
                    return -1;
            }
            catch
            {
                return -1;
            }
        }
        public List<Tuple<int, string>> GetListOrderQueueByDepartment(string Department, List<string> TaskFormCode)
        {
            try
            {
                Dictionary<int, string> rs = new Dictionary<int, string>();
                List<Tuple<int, string>> rs1 = new List<Tuple<int, string>>();
                TTS_DBEntities _data = new TTS_DBEntities();
                var lst = _data.TaskDirections.Where(n => n.SystemCode == Department && TaskFormCode.Contains(n.TaskFormCode)).ToList();
                //var rs = new List<int>();
                if (lst.Count > 0)
                {
                    foreach (var item in lst)
                    {
                        //var flag = rs.Where(n => n.Key == item.OrderQueue && n.Value == item.TaskFormCode).FirstOrDefault();
                        //if (flag.Key==0 && flag.Value == null)
                        //{
                        //    rs.Add( Convert.ToInt32(item.OrderQueue), item.TaskFormCode );
                        //}

                        var flag1 = rs1.Where(n => n.Item1 == item.OrderQueue && n.Item2 == item.TaskFormCode).FirstOrDefault();
                        if (flag1== null)
                        {
                            rs1.Add(new Tuple<int, string>(Convert.ToInt32(item.OrderQueue), item.TaskFormCode));
                        }
                    }
                    return rs1;
                }
                    //return Convert.ToInt32(lst[0].OrderQueue);
                else
                    return new List<Tuple<int, string>>();
            }
            catch
            {
                return new List<Tuple<int, string>>();
            }
        }
        public int GetOrderQueueByDepartment(string TaskFormCode,string Department)
        {
            try
            {
                TTS_DBEntities _data = new TTS_DBEntities();
                var lst = _data.TaskDirections.Where(n => n.SystemCode == Department && n.TaskFormCode == TaskFormCode).ToList();
                if (lst.Count > 0)
                    return Convert.ToInt32(lst[0].OrderQueue);
                else
                    return -1;
            }
            catch
            {
                return -1;
            }
        }
        public List<TaskDirection> GetListPermissionAdmin(string TaskFormCode, int OrderQueue)
        {
            try
            {
                TTS_DBEntities _data = new TTS_DBEntities();
                var itemFirst = 0;
                for (int i = OrderQueue - 1; i > 0; i--)
                {
                    var item = _data.TaskDirections.Where(n => n.OrderQueue == i && n.TaskFormCode == TaskFormCode).FirstOrDefault();
                    if (item.IsActive != null && item.IsActive == true)
                    {
                        itemFirst = Convert.ToInt32(item.OrderQueue);
                        break;
                    }
                }
                var itemLast = 0;
                for (int i = OrderQueue + 1; i < 20; i++)
                {
                    var item = _data.TaskDirections.Where(n => n.OrderQueue == i && n.TaskFormCode == TaskFormCode).FirstOrDefault();
                    if (item == null)
                    {
                        break;
                    }
                    if (item.IsActive != null && item.IsActive == true)
                    {
                        itemLast = Convert.ToInt32(item.OrderQueue);
                        break;
                    }
                }
                return _data.TaskDirections.Where(n => (n.TaskFormCode == TaskFormCode) && (n.OrderQueue == OrderQueue || (n.OrderQueue == itemFirst && n.IsValid == true) || (n.OrderQueue == itemLast && n.IsValid == false))).ToList();
            }
            catch
            {
                return new List<TaskDirection>();
            }
        }


    }
}
