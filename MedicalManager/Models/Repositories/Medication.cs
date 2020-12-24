using MedicalManager.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedicalManager.Areas.Identity.Data;

namespace MedicalManager.Models.Repositories
{
    public class Medication : IMedication
    {

        

        public Models.Medication AddMedication(Models.Medication medication, int UserId)
        {
            throw new NotImplementedException();
        }

        public Models.Medication DeleteMedication(int id, int UserId)
        {
            throw new NotImplementedException();
        }

        public Models.Medication EditMedication(Models.Medication medication, int UserId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Models.Medication> GetAllMedication(int UserId)
        {
            throw new NotImplementedException();
        }

        public Models.Medication GetMedication(int Id, int UserId)
        {
            throw new NotImplementedException();
        }

        public Models.Medication UpdateMedication(Models.Medication medication, int UserId)
        {
            throw new NotImplementedException();
        }
    }
}
