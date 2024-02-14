using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AloFinances.Domain.Entity
{
    public enum StatusConta
    {
        PENDENTE = 0,
        RECEBIDA = 1,
        ATRASADA = 2,
        CANCELADA = 10
    }
}
