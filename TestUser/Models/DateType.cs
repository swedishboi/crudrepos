using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using TestUser.DTO;
using TestUser.DAL;

namespace TestUser.Models
{
    public class DateType
    {
        public int dateTypeId { get; set; }
        public string dateTypeName { get; set; }

        public List<DateType> GetAll()
        {
            List<DateTypeDTO> dateTypesDTOList = new DateTypeRepository().SelectAll();
            if (dateTypesDTOList == null)
                return null;
            List<DateType> dateTypesModelsList = new List<DateType>();
            foreach (var i in dateTypesDTOList)
            {
                dateTypesModelsList.Add(new DateType
                {
                    dateTypeId = i.dateTypeId,
                    dateTypeName = i.dateTypeName
                });
            }
            return dateTypesModelsList;
        }
    }
}