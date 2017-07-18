using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using TestUser.DTO;
using TestUser.DAL;

namespace TestUser.Models
{
    public class Characteristic
    {
        public int charactersticId { get; set; }
        public int itemTypeId { get; set; }
        public string characteristicName { get; set; }
        public string notation { get; set; }
        public int dateTypeId { get; set; }
        public string characteristicValue { get; set; }
        public int unitId { get; set; }

        public List<Characteristic> GetAll()
        {
            List<CharacteristicDTO> characteristicsDTOList = new CharacteristicRepository().SelectAll();
            if (characteristicsDTOList == null)
                return null;
            List<Characteristic> characteristicsModelsList = new List<Characteristic>();
            foreach (var i in characteristicsDTOList)
            {
                characteristicsModelsList.Add(new Characteristic
                {
                    charactersticId = i.charactersticId,
                    itemTypeId = i.itemTypeId,
                    characteristicName = i.characteristicName,
                    notation = i.notation,
                    dateTypeId = i.dateTypeId,
                    characteristicValue = i.characteristicValue,
                    unitId= i.unitId
                });
            }
            return characteristicsModelsList;
        }
        public List<Characteristic> GetByItemId(int _id)
        {
            List<CharacteristicDTO> characteristicsDTOList = new CharacteristicRepository().SelectByItemId(_id);
            if (characteristicsDTOList == null)
                return null;
            List<Characteristic> characteristicsModelsList = new List<Characteristic>();
            foreach (var i in characteristicsDTOList)
            {
                characteristicsModelsList.Add(new Characteristic
                {
                    charactersticId = i.charactersticId,
                    itemTypeId = i.itemTypeId,
                    characteristicName = i.characteristicName,
                    notation = i.notation,
                    dateTypeId = i.dateTypeId,
                    characteristicValue = i.characteristicValue,
                    unitId= i.unitId
                });
            }
            return characteristicsModelsList;
        }
    }
}