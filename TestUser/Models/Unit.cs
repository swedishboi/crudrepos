using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using TestUser.DTO;
using TestUser.DAL;

namespace TestUser.Models
{
    public class Unit
    {
        public int unitId { get; set; }
        public int characteristicId { get; set; }
        public string unitName { get; set; }
        public List<Unit> GetAll()
        {
            List<UnitDTO> unitsDTOList = new UnitRepository().SelectAll();
            if (unitsDTOList == null)
                return null;
            List<Unit> unitsModelsList = new List<Unit>();
            foreach (var i in unitsDTOList)
            {
                unitsModelsList.Add(new Unit
                {
                    unitId = i.unitId,
                    characteristicId = i.characteristicId,
                    unitName = i.unitName
                });
            }
            return unitsModelsList;
        }
    }
}