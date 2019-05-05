using ProjectTreinritten.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ProjectTreinritten.Repository
{
    public class RitDAO
    {
        private readonly TreinProjectContext _db;

        public RitDAO()
        {
            _db = new TreinProjectContext();
        }

        public IEnumerable<Rit> GetAll()
        {
            return _db.Rit.ToList();
        }

        public Rit Get(int id)
        {
            return _db.Rit.Where(b => b.RitId == id).First();
        }

        public IEnumerable<Rit> GetAllByCities(int VertrekId, int EindId)
        {
            return _db.Rit.Where(b => b.VertrekStationId == VertrekId && b.AankomstStationId == EindId).ToList();
        }

        public void Update(Rit entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Create(Rit entity)
        {
            _db.Entry(entity).State = EntityState.Added;
            _db.SaveChanges();
        }
    }
}
