using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AloDoutor.Core.Messages
{
    public class RelatorioEvent
    {
        public RelatorioEvent(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
