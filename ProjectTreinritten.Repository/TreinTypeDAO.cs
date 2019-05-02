using ProjectTreinritten.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ProjectTreinritten.Repository
{
    public class TreinTypeDAO
    {
        private readonly TreinProjectContext _db;

        public TreinTypeDAO()
        {
            _db = new TreinProjectContext();
        }

        public IEnumerable<TreinType> GetAll()
        {
            return _db.TreinType.ToList();
        }

        public TreinType Get(int id)
        {
            return _db.TreinType.Where(b => b.TreinTypeId == id).First();
        }

        public void Update(TreinType entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Create(TreinType entity)
        {
            _db.Entry(entity).State = EntityState.Added;
            _db.SaveChanges();
        }
    }
}
