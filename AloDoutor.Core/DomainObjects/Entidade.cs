using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AloDoutor.Core.DomainObjects
{
    public abstract class Entidade
    {
        public Guid Id { get; set; }

        protected Entidade()
        {
            Id = Guid.NewGuid();
        }
    }
}
