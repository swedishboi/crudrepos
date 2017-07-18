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
        public int positionId { get; set; }
        public string positionName { get; set; }
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
                    positionId = i.positionId,
                    positionName = i.positionName
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
        public bool Add(string _name)
        {
            bool flag = new PositionRepository().Insert(_name);
            return flag;
        }
    }
}