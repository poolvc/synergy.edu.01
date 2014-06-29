using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Synergy.Data.Edu;
using Synergy.Domain.Edu.Entities;



namespace Synergy.Application.Edu
{
    public class BLAlumnos
    {
        //Objeto transaccion
        DLTransactionMng _transaccion = null;
        DLAlumno _dl = new DLAlumno();

        /// <summary>
        /// Método extrae Companias por Usuario
        /// </summary>
        /// <returns>Devuelve un DataSet</returns>
        public DataSet Listar(BEAlumnos pbe)
        {
            return _dl.Listar(pbe);
        }

        public BEAlumnos Insetar(List<string> mLista, string pstrPeriodoAcademico, string pstrUsuarioCreacion)
        {
            BEAlumnos pbe = new BEAlumnos();

                for (int i = 0; i < mLista.Count; i++)
                {
                    pbe = _dl.Insetar(mLista[i].ToString(), pstrPeriodoAcademico, pstrUsuarioCreacion);
                }
           
            return pbe;
        }

        public BEAlumnos InsetarMasivo(BEAlumnos obe)
        {
            BEAlumnos pbe = new BEAlumnos();
            pbe = _dl.InsetarMasivo(obe);
            return pbe;
        }

    }
}
