using ProjectTreinritten.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Globalization;
using Microsoft.EntityFrameworkCore;

namespace ProjectTreinritten.Repository
{
    public class VakantieDagenDAO
    {
        private readonly TreinProjectContext _db;

        public VakantieDagenDAO()
        {
            _db = new TreinProjectContext();
        }

        public IEnumerable<VakantieDagen> GetAll()
        {
            return _db.VakantieDagen.ToList();
        }

        public VakantieDagen Get(DateTime dag)
        {

            //"https://stackoverflow.com/questions/12257483/c-sharp-how-to-convert-any-date-format-to-yyyy-mm-dd/12257514"
            string dagString = dag.ToString();
            IFormatProvider culture = new CultureInfo("nl-be", true);
            DateTime dateVal = DateTime.ParseExact(dagString, "yyyy-MM-dd", culture);

            return _db.VakantieDagen.Where(b => b.VakantieDag == dateVal).First();
        }

        public void Update(VakantieDagen entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Create(VakantieDagen entity)
        {
            _db.Entry(entity).State = EntityState.Added;
            _db.SaveChanges();
        }
    }
}
