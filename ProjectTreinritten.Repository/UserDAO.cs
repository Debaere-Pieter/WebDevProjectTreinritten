using Microsoft.EntityFrameworkCore;
using ProjectTreinritten.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectTreinritten.Repository
{
    public class UserDAO
    {
        private readonly TreinProjectContext _db;

        public UserDAO()
        {
            _db = new TreinProjectContext();
        }

        public IEnumerable<AspNetUsers> GetAll()
        {
            return _db.AspNetUsers.ToList();
        }

        public AspNetUsers Get(string id)
        {
            return _db.AspNetUsers.Where(b => b.Id == id).First();
        }

        public void Update(AspNetUsers entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Create(AspNetUsers entity)
        {
            _db.Entry(entity).State = EntityState.Added;
            _db.SaveChanges();
        }
    }
}
