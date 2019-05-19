using Microsoft.EntityFrameworkCore;
using ProjectTreinritten.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectTreinritten.Repository
{
    public class ZetelDAO
    {
        private readonly TreinProjectContext _db;

        public ZetelDAO()
        {
            _db = new TreinProjectContext();
        }

        public IEnumerable<Zetels> GetAll()
        {
            return _db.Zetels.ToList();
        }

        public Zetels Get(int id)
        {
            return _db.Zetels.Where(b => b.ZetelId == id).First();
        }

        public void Update(Zetels entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Create(Zetels entity)
        {
            _db.Entry(entity).State = EntityState.Added;
            _db.SaveChanges();
        }
    }
}
