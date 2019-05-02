using ProjectTreinritten.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ProjectTreinritten.Repository
{
    public class StationDAO
    {
        private readonly TreinProjectContext _db;

        public StationDAO()
        {
            _db = new TreinProjectContext();
        }

        public IEnumerable<Station> GetAll()
        {
            return _db.Station.ToList();
        }

        public Station Get(int id)
        {
            return _db.Station.Where(b => b.StationId == id).First();
        }

        public void Update(Station entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Create(Station entity)
        {
            _db.Entry(entity).State = EntityState.Added;
            _db.SaveChanges();
        }
    }
}
