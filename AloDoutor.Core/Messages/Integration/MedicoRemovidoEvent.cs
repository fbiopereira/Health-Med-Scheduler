using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AloDoutor.Core.Messages.Integration
{
    public class MedicoRemovidoEvent
    {
        public MedicoRemovidoEvent(string cpfMedico, string crm)
        {
            CpfMedico = cpfMedico;
            Crm = crm;
        }

        public string CpfMedico { get; private set; }  
        public string Crm { get; private set; }
        
    }
}
