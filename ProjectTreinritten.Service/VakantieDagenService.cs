using ProjectTreinritten.Domain.Entities;
using ProjectTreinritten.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTreinritten.Service
{
    public class VakantieDagenService
    {
        private VakantieDagenDAO vakantieDagenDAO;

        public VakantieDagenService()
        {
            vakantieDagenDAO = new VakantieDagenDAO();
        }

        public IEnumerable<VakantieDagen> GetAll()
        {
            return vakantieDagenDAO.GetAll();
        }

        public VakantieDagen Get(DateTime dag)
        {
            return vakantieDagenDAO.Get(dag);
        }

        public void Update(VakantieDagen entity)
        {
            vakantieDagenDAO.Update(entity);
        }

        public void Create(VakantieDagen entity)
        {
            vakantieDagenDAO.Create(entity);
        }
    }
}
