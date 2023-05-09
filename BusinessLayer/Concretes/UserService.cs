using BusinessLayer.Abstract;
using DataAccessLayer.Abstracts;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concretes
{
    public class UserService : IUserService
    {
        private readonly IRepository repo;

        public UserService(IRepository repo)
        {
            this.repo = repo;
        }

        public int CreateUser(User user)
        {
            repo.Add(user);
            return repo.SaveChanges();
        }

        public int DeleteUser(int id)
        {
            var user = GetUserById(id);
            repo.Remove(user);
            return repo.SaveChanges();
        }

        public int DeleteUser(User user)
        {
            repo.Remove(user);
            return repo.SaveChanges();
        }

        public List<User> GetAllUsers()
        {
            return repo.GetIQueryableObject<User>().ToList();
        }

        public Role GetRoleByUserId(int userId)
        {
            var result = (from r in repo.GetIQueryableObject<Role>()
                          join u in repo.GetIQueryableObject<User>() on r.RoleId equals u.RoleId
                          where u.UserId == userId
                          select r).FirstOrDefault();
            return result;
        }

        public User GetUser(string username, string password)
        {
            return repo.GetEntityObject<User>(x => x.UserName == username && x.Password == password);
        }

        public User GetUserById(int id)
        {
            return repo.GetEntityObject<User>(id);
        }

        public int UpdateUser(User user)
        {
            repo.Modify(user);
            return repo.SaveChanges();
        }
    }
}
