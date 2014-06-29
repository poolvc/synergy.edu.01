using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Synergy.Data.Edu;
using Synergy.Domain.Edu.Entities;



namespace Synergy.Application.Edu
{
    public class BLFamilia
    {
        //Objeto transaccion
        DLTransactionMng _transaccion = null;
        DLFamilia _dl = new DLFamilia();

        /// <summary>
        /// Método extrae Companias por Usuario
        /// </summary>
        /// <returns>Devuelve un DataSet</returns>
        public DataSet Listar(BEFamilia pbe)
        {
            return _dl.Listar(pbe);
        }

        public BEFamilia Insetar(List<BEFamilia> mLista, string pstrPeriodoAcademico, string pstrUsuarioCreacion)
        {
            BEFamilia pbe = new BEFamilia();

                foreach (BEFamilia item in mLista)
                {
                    item.PeriodoAcademico = pstrPeriodoAcademico;
                    item.UsuarioCreacion = pstrUsuarioCreacion;
                    pbe = _dl.Insetar(item);
                }

            return pbe;
        }

        public BEFamilia InsetarMasivo(BEFamilia obe)
        {
            BEFamilia pbe = new BEFamilia();
            pbe = _dl.InsetarMasivo(obe);
            return pbe;
        }

    }
}
