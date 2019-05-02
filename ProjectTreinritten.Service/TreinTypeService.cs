using ProjectTreinritten.Domain.Entities;
using ProjectTreinritten.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTreinritten.Service
{
    public class TreinTypeService
    {
        private TreinTypeDAO treinTypeDAO;

        public TreinTypeService()
        {
            treinTypeDAO = new TreinTypeDAO();
        }

        public IEnumerable<TreinType> GetAll()
        {
            return treinTypeDAO.GetAll();
        }

        public TreinType Get(int id)
        {
            return treinTypeDAO.Get(id);
        }

        public void Update(TreinType entity)
        {
            treinTypeDAO.Update(entity);
        }

        public void Create(TreinType entity)
        {
            treinTypeDAO.Create(entity);
        }
    }
}
