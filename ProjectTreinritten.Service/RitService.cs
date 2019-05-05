using ProjectTreinritten.Domain.Entities;
using ProjectTreinritten.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTreinritten.Service
{
    public class RitService
    {
        private RitDAO ritDAO;

        public RitService()
        {
            ritDAO = new RitDAO();
        }

        public IEnumerable<Rit> GetAll()
        {
            return ritDAO.GetAll();
        }

        public Rit Get(int id)
        {
            return ritDAO.Get(id);
        }

        public Rit GetByCities(int VertrekId, int EindId)
        {
            return ritDAO.GetByCities(VertrekId, EindId);
        }

        public void Update(Rit entity)
        {
            ritDAO.Update(entity);
        }

        public void Create(Rit entity)
        {
            ritDAO.Create(entity);
        }
    }
}
