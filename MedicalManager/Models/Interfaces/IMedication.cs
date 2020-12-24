using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalManager.Models.Interfaces
{
    public interface IMedication
    {
        Medication GetMedication(int Id, int UserId);
        IEnumerable<Medication> GetAllMedication(int UserId);
        Medication AddMedication(Medication medication, int UserId);
        Medication UpdateMedication(Medication medication, int UserId);
        Medication DeleteMedication(int id, int UserId);
        Medication EditMedication(Medication medication, int UserId);
    }
}
