using ProjectTreinritten.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ProjectTreinritten.Repository
{
    public class TrajectDAO
    {
        private readonly TreinProjectContext _db;

        public TrajectDAO()
        {
            _db = new TreinProjectContext();
        }

        public IEnumerable<Traject> GetAll()
        {
            return _db.Traject.ToList();
        }

        public Traject Get(int id)
        {
            return _db.Traject.Where(b => b.TrajectId == id).First();
        }

        public Traject GetByRit(Rit r)
        {
            return _db.Traject.Where(b => b.Rit1Id == r.RitId).First();
        }

        public IEnumerable<Traject> GetTrajecten1Rit(int rit1Id)
        {
                return _db.Traject.Where(b => b.Rit1Id == rit1Id && b.Rit2Id == null && b.Rit3Id == null).ToList();
        }

        public IEnumerable<Traject> GetTrajecten2Rit(int rit1Id, int rit2Id)
        {
            return _db.Traject.Where(b => b.Rit1Id == rit1Id && b.Rit2Id == rit2Id && b.Rit3Id == null).ToList();
        }

        public IEnumerable<Traject> GetTrajecten3Rit(int rit1Id, int rit2Id, int rit3Id)
        {
            return _db.Traject.Where(b => b.Rit1Id == rit1Id && b.Rit2Id == rit2Id && b.Rit3Id == rit3Id).ToList();
        }

        public void Update(Traject entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Create(Traject entity)
        {
            _db.Entry(entity).State = EntityState.Added;
            _db.SaveChanges();
        }
    }
}
