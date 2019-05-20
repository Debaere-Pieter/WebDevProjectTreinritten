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

        public IEnumerable<Rit> GetAllByArrivalCity(int Id)
        {
            return _db.Rit.Where(b => b.AankomstStationId == Id).ToList();
        }

        public IEnumerable<Rit> GetAllByDepartCity(int Id)
        {
            return _db.Rit.Where(b => b.VertrekStationId == Id).ToList();
        }

        public IEnumerable<Rit> GetAllByCities(int VertrekId, int EindId)
        {
            return _db.Rit.Where(b => b.VertrekStationId == VertrekId && b.AankomstStationId == EindId).ToList();
        }

        public IEnumerable<Rit> GetAllByCitiesWithDate(int VertrekId, int EindId, DateTime date)
        {
            var list =  _db.VakantieDagen.ToList();
            List<DateTime> dagen = new List<DateTime>();

            for(int i = 0; i < list.Count; i++)
            {
                dagen.Add(list[i].VakantieDag);
            }
            bool extraPlaats = false;

            if (dagen.Contains(date) && (EindId == 2 || EindId == 4) && (date.Month == 11 || date.Month == 12))
            {
                extraPlaats = true;
            }
            if (dagen.Contains(date) && date.Month == 4 && (EindId == 3 || EindId == 4 || EindId == 6))
            {
                extraPlaats = true;
            }

            //dit op true zetten
            if (extraPlaats)
            {
                return _db.Rit.Where(b => b.VertrekStationId == VertrekId && b.AankomstStationId == EindId && b.TreinTypeId == 2).ToList();
            }
            else
            {
                return _db.Rit.Where(b => b.VertrekStationId == VertrekId && b.AankomstStationId == EindId && b.TreinTypeId == 1).ToList();
            }
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

        public IEnumerable<Rit> GetRitByCitiesWithDateAndTime(int VertrekId, int EindId, DateTime date, TimeSpan tijd)
        {
            var list = _db.VakantieDagen.ToList();

            List<DateTime> dagen = new List<DateTime>();

            for (int i = 0; i < list.Count; i++)
            {
                dagen.Add(list[i].VakantieDag);
            }

            bool extraPlaats = false;

            if(dagen.Contains(date) && (EindId == 2 || EindId == 4) && (date.Month ==11 || date.Month == 12))
            {
                extraPlaats = true;
            }
            if(dagen.Contains(date) && date.Month == 4 && (EindId == 3 || EindId == 4 || EindId == 6))
            {
                extraPlaats = true;
            }

            //dit op true zetten
            if (extraPlaats)
            {
                return _db.Rit.Where(b => b.VertrekStationId == VertrekId && b.AankomstStationId == EindId && b.TreinTypeId == 2 && b.VertrekUur.Hours> tijd.Hours).ToList();
            }
            else
            {
                return _db.Rit.Where(b => b.VertrekStationId == VertrekId && b.AankomstStationId == EindId && b.TreinTypeId == 1 && b.VertrekUur.Hours>tijd.Hours).ToList();
            }
        }

        public TreinType GetTreinTypeRit(int id)
        {
            var rit = _db.Rit.Where(b => b.RitId == id).First();
            return _db.TreinType.Where(b => b.TreinTypeId == rit.TreinTypeId).First();
        }
    }
}
