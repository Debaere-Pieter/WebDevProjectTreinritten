using ProjectTreinritten.Domain.Entities;
using ProjectTreinritten.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTreinritten.Service
{
    public class TrajectService
    {
        private TrajectDAO trajectDAO;

        public TrajectService()
        {
            trajectDAO = new TrajectDAO();
        }

        public IEnumerable<Traject> GetAll()
        {
            return trajectDAO.GetAll();
        }

        public Traject Get(int id)
        {
            return trajectDAO.Get(id);
        }

        public Traject GetByRit(Rit r)
        {
            return trajectDAO.GetByRit(r);
        }

        public void Update(Traject entity)
        {
            trajectDAO.Update(entity);
        }

        public void Create(Traject entity)
        {
            trajectDAO.Create(entity);
        }
        public IEnumerable<Traject> GetTrajecten1Rit(int rit1Id)
        {
            return trajectDAO.GetTrajecten1Rit(rit1Id);
        }

        public IEnumerable<Traject> GetTrajecten2Rit(int rit1Id, int rit2Id)
        {
            return trajectDAO.GetTrajecten2Rit(rit1Id, rit2Id);
        }

        public IEnumerable<Traject> GetTrajecten3Rit(int rit1Id, int rit2Id, int rit3Id)
        {
            return trajectDAO.GetTrajecten3Rit(rit1Id, rit2Id, rit3Id);
        }



    }
}
