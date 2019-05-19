using ProjectTreinritten.Domain.Entities;
using ProjectTreinritten.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTreinritten.Service
{
    public class BoekingService
    {
        private BoekingDAO boekingDAO;

        public BoekingService()
        {
            boekingDAO = new BoekingDAO();
        }

        public IEnumerable<Boeking> GetAll()
        {
            return boekingDAO.GetAll();
        }

        public IEnumerable<Boeking> GetAllByUser(string id)
        {
            return boekingDAO.GetAllByUser(id);
        }

        public IEnumerable<Boeking> GetAllByDate(DateTime d)
        {
            return boekingDAO.GetAllByDate(d);
        }

        public Boeking Get(int id)
        {
            return boekingDAO.Get(id);
        }

        public void Update(Boeking entity)
        {
            boekingDAO.Update(entity);
        }

        public void Create(Boeking entity)
        {

            boekingDAO.Create(entity);

        }

        public void Delete(Boeking entity)
        {
            boekingDAO.Delete(entity);
        }
    }
}
