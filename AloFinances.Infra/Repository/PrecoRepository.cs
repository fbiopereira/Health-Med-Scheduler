using AloFinances.Domain.Entity;
using AloFinances.Domain.Interfaces;
using AloFinances.Infra.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AloFinances.Infra.Repository
{
    public class PrecoRepository : Repository<Preco>, IPrecoRepository
    {
        public PrecoRepository(AloFinancesContext db) : base(db)
        {
        }
    }
}
