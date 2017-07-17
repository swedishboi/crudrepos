using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using TestUser.DTO;
using TestUser.DAL;

namespace TestUser.Models
{
    public class Position
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<Position> GetAll()
        {
            List<PositionDTO> positionsDTOList = new PositionRepository().SelectAll();
            if (positionsDTOList == null)
                return null;
            List<Position> positionsModelsList = new List<Position>();
            foreach (var i in positionsDTOList)
            {
                positionsModelsList.Add(new Position
                {
                    id = i.id,
                    name = i.name
                });
            }
            return positionsModelsList;
        }
        public bool Remove(int _id)
        {
            bool flag = new PositionRepository().Delete(_id);
            return flag;
        }
        public bool Edit(int _id, string _name)
        {
            bool flag = new PositionRepository().Update(_id, _name);
            return flag;
        }
        public Position Add(string _name)
        {
            PositionDTO positionDTO = new PositionRepository().Create(_name);
            if (positionDTO == null)
                return null;
            Position positionModel = new Position
            {
                name = positionDTO.name
            };
            return positionModel;
        }
    }
}