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

        public IEnumerable<Rit> GetAllByArrivalCity(int Id)
        {
            return ritDAO.GetAllByArrivalCity(Id);
        }

        public IEnumerable<Rit> GetAllByDepartCity(int Id)
        {
            return ritDAO.GetAllByDepartCity(Id);
        }

        public IEnumerable<Rit> GetAllByCities(int VertrekId, int EindId)
        {
            return ritDAO.GetAllByCities(VertrekId, EindId);
        }

        public IEnumerable<Rit> GetAllByCitiesWithDate(int VertrekId, int EindId, DateTime date)
        {
            return ritDAO.GetAllByCitiesWithDate(VertrekId, EindId, date);
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
