using System;
using System.Collections.Generic;
using System.Text;
using ProjectTreinritten.Domain.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ProjectTreinritten.Repository
{
    public class BoekingDAO
    {
        private readonly TreinProjectContext _db;

        public BoekingDAO()
        {
            _db = new TreinProjectContext();
        }

        public IEnumerable<Boeking> GetAll()
        {
            return _db.Boeking.ToList();
        }

        public Boeking Get(int id)
        {
            return _db.Boeking.Where(b => b.BoekingId == id).First();
        }

        public IEnumerable<Boeking> GetAllByUser(string id)
        {
            return _db.Boeking.Where(b => b.LoginId == id).ToList();
        }

        public IEnumerable<Boeking> GetAllByDate(DateTime d)
        {
            return _db.Boeking.Where(b => b.VertrekDatum == d);
        }

        public void Update(Boeking entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Create(Boeking entity)
        {
            _db.Entry(entity).State = EntityState.Added;
            _db.SaveChanges();
        }

        public void Delete(Boeking entity)
        {
            _db.Entry(entity).State = EntityState.Deleted;
            _db.SaveChanges();
        }
    }
}
