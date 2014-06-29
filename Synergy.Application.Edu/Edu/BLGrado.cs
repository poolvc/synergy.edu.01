using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Synergy.Data.Edu;
using Synergy.Domain.Edu.Entities;

namespace Synergy.Application.Edu
{
    public class BLGrado// : IDisposable
    {
        DLGrado _dl = new DLGrado();



        public DataSet Listar()
        {
            return _dl.Listar();
        }

 
    }
}
