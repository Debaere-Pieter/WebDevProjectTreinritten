using ProjectTreinritten.Domain.Entities;
using ProjectTreinritten.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTreinritten.Service
{
    public class ZetelService
    {
        private ZetelDAO zetelDAO;

        public ZetelService()
        {
            zetelDAO = new ZetelDAO();
        }

        public IEnumerable<Zetels> GetAll()
        {
            return zetelDAO.GetAll();
        }

        public Zetels Get(int id)
        {
            return zetelDAO.Get(id);
        }

        public void Update(Zetels entity)
        {
            zetelDAO.Update(entity);
        }

        public void Create(Zetels entity)
        {
            zetelDAO.Create(entity);
        }
    }
}
