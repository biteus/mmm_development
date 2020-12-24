using MedicalManager.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalManager.Models
{
    public class User: MedicalManagerUser
    {
        public User()
        {
            Medications = new HashSet<Medication>();
        }

        public virtual  ICollection<Medication> Medications { get; set; }
    }
}
