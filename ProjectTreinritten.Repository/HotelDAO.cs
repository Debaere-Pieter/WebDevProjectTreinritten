using ProjectTreinritten.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ProjectTreinritten.Repository
{
    public class HotelDAO
    {
        private readonly TreinProjectContext _db;

        public HotelDAO()
        {
            _db = new TreinProjectContext();
        }

        public IEnumerable<Hotel> GetAll()
        {
            return _db.Hotel.ToList();
        }

        public Hotel Get(int id)
        {
            return _db.Hotel.Where(b => b.HotelId == id).First();
        }

        public void Update(Hotel entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Create(Hotel entity)
        {
            _db.Entry(entity).State = EntityState.Added;
            _db.SaveChanges();
        }
    }
}
