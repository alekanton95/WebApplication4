using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication4.Models;
using Newtonsoft.Json;
using System.Data.Entity;

namespace WebApplication4
{
    public class SaveDataFromContactInfo
    {
        private Modeldb db = new Modeldb();
        ContactList ContactList;
        public SaveDataFromContactInfo(ContactList contactList, Modeldb db)
        {
            ContactList = contactList;
        }
        /// <summary>
        /// Сохранение данных из формы посредством json
        /// </summary>
        /// <param name="jsonFile"></param>
        public void SaveData(string jsonFile)
        {
            var json = JsonConvert.DeserializeObject<IEnumerable<ContactInfo>>(jsonFile);
            foreach (var item in json)
            {
                if(item.ContactListId==null)
                {
                    //item.ContactList = ContactList;
                    item.ContactListId = ContactList.Id;
                    db.ContactInfoes.Add(item);
                }
                else
                {
                    db.Entry(item).State = EntityState.Modified;
                    
                }
                db.SaveChanges();
            }

        }
    }
}