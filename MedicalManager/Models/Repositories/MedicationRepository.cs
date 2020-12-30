﻿using MedicalManager.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedicalManager.Areas.Identity.Data;
using MedicalManager.Data;
using MedicalManager.Models;
using Microsoft.Extensions.Logging;

namespace MedicalManager.Models.Repositories
{
    public class MedicationRepository : IMedication
    {
        private readonly MedicalManagerDBContext _dbContext;
        private readonly ILogger<MedicationRepository> _logger;

        public MedicationRepository(MedicalManagerDBContext dbContext,  ILogger<MedicationRepository> logger)
        {
            this._dbContext = dbContext;
            this._logger = logger;
        }


        public Medication AddMedication(Medication medication, string UserId)
        {
            medication.UerID = UserId;
            _dbContext.Medications.Add(medication);
            _dbContext.SaveChanges();
            return medication;
        }

        public Models.Medication DeleteMedication(int id, string UserId)
        {
            Medication medication = _dbContext.Medications.Where(item => item.Id == id && item.UerID.Contains(UserId)).FirstOrDefault();
            if (medication != null)
            {
                _dbContext.Medications.Remove(medication);
                _dbContext.SaveChanges();
            }
            return medication;
        }

        public Models.Medication EditMedication(Models.Medication medication, string UserId)
        {
            medication.UerID = UserId;
            _dbContext.Medications.Update(medication);
            _dbContext.SaveChanges();
            return medication;
        }

        public IEnumerable<Models.Medication> GetAllMedication(string UserId)
        {
            return _dbContext.Medications.Where(m => m.UerID.Contains(UserId));
        }

        public Models.Medication GetMedication(int Id, string UserId)
        {
            Medication medication = _dbContext.Medications.Where(item => item.Id == Id && item.UerID.Contains(UserId)).FirstOrDefault();
            return medication;

        }

        public Medication UpdateMedication(Medication medication, string UserId)
        {
            //Medication medication = _dbContext.Medications.Where(item => item.Id == id && item.UerID.Contains(UserId)).FirstOrDefault();
            medication.UerID = UserId;
            if (medication != null)
            {
                var updatedMedication = _dbContext.Medications.Attach(medication);
                updatedMedication.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                updatedMedication.Context.SaveChanges();
                //updatedMedication.Entry(myedication).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                //updatedMedication.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                //_dbContext.SaveChanges();
            }
            
            return medication;
        }
    }
}
