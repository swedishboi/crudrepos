using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using TestUser.DTO;

namespace TestUser.DAL
{
    public class CharacteristicRepository
    {
        string sqlExpSelect = 
            "select T_ItemTypeCharacteristic.*, T_CharacteristicValue.value, T_CharacteristicValue.unitId from T_CharacteristicValue inner join T_ItemTypeCharacteristic on T_CharacteristicValue.characteristicsId = T_ItemTypeCharacteristic.characteristicsId";
        string sqlExpSelectByItemId =
            "select T_ItemTypeCharacteristic.*, T_CharacteristicValue.value, T_CharacteristicValue.unitId from T_ItemTypeCharacteristic inner join T_CharacteristicValue on T_ItemTypeCharacteristic.characteristicsId = T_CharacteristicValue.characteristicsId inner join T_Item on T_CharacteristicValue.itemId = T_Item.itemId where T_Item.itemId = @itemId";
        public List<CharacteristicDTO> SelectAll()
        {
            List<CharacteristicDTO> characteristicsDTOList = null;
            using (SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(sqlExpSelect, connect))
                {
                    connect.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            characteristicsDTOList = new List<CharacteristicDTO>();
                            while (reader.Read())
                            {
                                characteristicsDTOList.Add(new CharacteristicDTO
                                {
                                    charactersticId = reader.GetInt32(0),
                                    itemTypeId = reader.GetInt32(1),
                                    characteristicName = reader.GetString(2),
                                    notation = reader.GetString(3),
                                    dateTypeId = reader.GetInt32(4),
                                    characteristicValue = reader.GetString(5),
                                    unitId = reader.GetInt32(6)
                                });
                            }
                        }
                    }
                }
                connect.Close();
            }
            return characteristicsDTOList;
        }
        public List<CharacteristicDTO> SelectByItemId(int _id)
        {
            List<CharacteristicDTO> characteristicsDTOList = null;
            using (SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(sqlExpSelectByItemId, connect))
                {
                    command.Parameters.Add("@itemId", SqlDbType.Int).Value = _id;
                    connect.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            characteristicsDTOList = new List<CharacteristicDTO>();
                            while (reader.Read())
                            {
                                characteristicsDTOList.Add(new CharacteristicDTO()
                                {
                                    charactersticId = reader.GetInt32(0),
                                    itemTypeId = reader.GetInt32(1),
                                    characteristicName = reader.GetString(2),
                                    notation = reader.GetString(3),
                                    dateTypeId = reader.GetInt32(4),
                                    characteristicValue = reader.GetString(5),
                                    unitId = reader.GetInt32(6)
                                });
                            }
                        }
                    }
                }
                connect.Close();
            }
            return characteristicsDTOList;
        }
    }
}