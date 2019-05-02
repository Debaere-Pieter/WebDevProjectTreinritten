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

        public void Update(Traject entity)
        {
            trajectDAO.Update(entity);
        }

        public void Create(Traject entity)
        {
            trajectDAO.Create(entity);
        }
    }
}
