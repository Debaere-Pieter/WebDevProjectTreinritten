using ProjectTreinritten.Domain.Entities;
using ProjectTreinritten.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTreinritten.Service
{
    public class StationService
    {
        private StationDAO stationDAO;

        public StationService()
        {
            stationDAO = new StationDAO();
        }

        public IEnumerable<Station> GetAll()
        {
            return stationDAO.GetAll();
        }

        public Station Get(int id)
        {
            return stationDAO.Get(id);
        }

        public void Update(Station entity)
        {
            stationDAO.Update(entity);
        }

        public void Create(Station entity)
        {
            stationDAO.Create(entity);
        }
    }
}
