using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication4.Models;
using Newtonsoft.Json;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace WebApplication4
{
    public class SaveDataFromContactInfo
    {
        private Modeldb db = new Modeldb();
        ContactList ContactList;
        public SaveDataFromContactInfo(ContactList contactList, Modeldb db)
        {
            ContactList = contactList;
            this.db = db;
        }
        /// <summary>
        /// Сохранение данных из формы посредством json
        /// </summary>
        /// <param name="jsonFile"></param>
        public void SaveData(string jsonFile)
        {
            if (!string.IsNullOrWhiteSpace(jsonFile))
            {
                IEnumerable<ContactInfo> json = SetContactInfoes(jsonFile);
                foreach (var item in json.OrderByDescending(i => i.ContactListId))
                {
                    if (item.ContactListId == null)
                    {
                        //item.ContactList = ContactList;
                        item.ContactListId = ContactList.Id;
                        if (IsNull(item))
                        {
                            db.ContactInfoes.Add(item);
                            //db.SaveChanges();
                            ContactList.ContactInfoes.Add(item);
                            db.SaveChanges();
                        }

                    }
                    else
                    {
                        if (IsNull(item))
                        {

                            db.Entry(item).State = EntityState.Modified;
                            db.SaveChanges();
                        }


                    }

                }
                Delete(json);
            }
        }

        /// <summary>
        /// Удаление связного элементов
        /// </summary>
        /// <param name="contactInfos"></param>
        private void Delete(IEnumerable<ContactInfo> contactInfos)
        {
            List<int> oldContactinfo = new List<int>();
            var AllContactInfo = db.ContactInfoes.Where(i => i.ContactListId == ContactList.Id).ToList();
            AllContactInfo.ForEach(i => oldContactinfo.Add(i.Id));
            foreach (var item in contactInfos)
            {
                oldContactinfo.Remove(item.Id);
            }
            foreach (var item in oldContactinfo)
            {
                var contactInfo1 = db.ContactInfoes.Where(c => c.Id == item).ToList();
                foreach (var item1 in contactInfo1)
                {
                    db.Entry(item1).State = EntityState.Deleted;
                    //db.ContactInfoes.Remove(item1);
                }
                //
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Проверка на пустоту строки
        /// </summary>
        /// <param name="contactInfo"></param>
        /// <returns></returns>
        private bool IsNull(ContactInfo contactInfo)
        {
            if (string.IsNullOrWhiteSpace(contactInfo.Email) && string.IsNullOrWhiteSpace(contactInfo.Phone)
                && string.IsNullOrWhiteSpace(contactInfo.Skype) && string.IsNullOrWhiteSpace(contactInfo.Other))
            {
                //var element = ContactList.ContactInfoes.Where(i => i.Id == contactInfo.Id).FirstOrDefault();
                db.Entry(contactInfo).State = EntityState.Deleted;
                db.SaveChanges();
                return false;
            }
            else return true;
        }

        /// <summary>
        /// Формирование в модель записи ContactList
        /// </summary>
        /// <param name="jsonFile"></param>
        /// <returns></returns>
        public ContactList GetContactList(string jsonFile)
        {
            if (!string.IsNullOrWhiteSpace(jsonFile))
            {
                var json = SetContactInfoes(jsonFile);
                foreach (var item in json)
                {
                    ContactList.ContactInfoes.Add(item);
                }
            }
            return ContactList;
        }
        /// <summary>
        /// Валидация данных ContactInfo
        /// </summary>
        /// <param name="contactInfo"></param>
        /// <returns></returns>
        public string ValidateData(ContactInfo contactInfo)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(contactInfo);
            string errorresult = "";
            if (!Validator.TryValidateObject(contactInfo, context, results, true))
            {
                foreach (var error in results)
                {
                    errorresult += error.ErrorMessage;
                }
            }
            return errorresult;
        }

        /// <summary>
        /// Валидация ContactList
        /// </summary>
        /// <returns></returns>
        public string ValidateData()
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(ContactList);
            string errorresult = "";
            if (!Validator.TryValidateObject(ContactList, context, results, true))
            {
                foreach (var error in results)
                {
                    errorresult += error.ErrorMessage + '\n';
                }
            }
            return errorresult;
        }

        /// <summary>
        /// Валидация списком ContactInfo
        /// </summary>
        /// <param name="contactInfos"></param>
        /// <returns></returns>
        public string ValidateData(IEnumerable<ContactInfo> contactInfos)
        {
            string error = "";
            foreach (var item in contactInfos)
            {
                error += ValidateData(item) + "\n";
            }
            return error;

        }

        /// <summary>
        /// Формирование из JSON списка ContactInfo
        /// </summary>
        /// <param name="jsonFile"></param>
        /// <returns></returns>
        private IEnumerable<ContactInfo> SetContactInfoes(string jsonFile)
        {
            return JsonConvert.DeserializeObject<IEnumerable<ContactInfo>>(jsonFile);
        }

        /// <summary>
        /// Валидация ContactInfo из JSON
        /// </summary>
        /// <param name="jsonFile"></param>
        /// <returns></returns>
        public string ValidateContactInfoes(string jsonFile)
        {
            return ValidateData(SetContactInfoes(jsonFile));
        }

        /// <summary>
        /// Общая валидация ContactInfo и ContactList
        /// </summary>
        /// <param name="jsonFile"></param>
        /// <returns></returns>
        public string AllValid(string jsonFile)
        {
            return ValidateData() + '\n' + ValidateData(SetContactInfoes(jsonFile));
        }


    }
}