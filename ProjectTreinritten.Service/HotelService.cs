using ProjectTreinritten.Domain.Entities;
using ProjectTreinritten.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTreinritten.Service
{
    public class HotelService
    {
        private HotelDAO hotelDAO;

        public HotelService()
        {
            hotelDAO = new HotelDAO();
        }

        public IEnumerable<Hotel> GetAll()
        {
            return hotelDAO.GetAll();
        }

        public Hotel Get(int id)
        {
            return hotelDAO.Get(id);
        }

        public void Update(Hotel entity)
        {
            hotelDAO.Update(entity);
        }

        public void Create(Hotel entity)
        {
            hotelDAO.Create(entity);
        }
    }
}
