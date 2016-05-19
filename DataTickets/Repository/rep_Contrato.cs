using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using _Entidades;

namespace DataTickets.Repository
{
    public class rep_Contrato
    {
        public List<Contrato> _getContratos()
        {
            using(var ctx =new _Context())
            {
                return ctx.Contrato.ToList();
            }
        }

    }
}
