
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Common;
using System.Configuration;
using Synergy.BE.Edu;

namespace Synergy.DL.Edu
{


    public class DLSNT_Acciones : DLTransaction, IDLTransaccion<BESNT_Acciones>
    {
    
            
        #region Members
                        
                 
                /// <summary>
                /// Método Inserta un SNT_Acciones 
                /// </summary>
                /// <returns>Devuelve una entidad</returns>

                public BESNT_Acciones Insertar(BESNT_Acciones pbe)
                {
                    BESNT_Acciones be = new BESNT_Acciones();
                    SqlDatabase sqlDB = new SqlDatabase(CadenaConexion.Obtener());
                    DbCommand dbCmd = sqlDB.GetStoredProcCommand("uSP_GALEXITO_CTB_SNT_Acciones_Insertar");
                        
                    try
                    {
                         dbCmd.CommandTimeout = CadenaConexion.CommandTimeout;
                        
                        
                        sqlDB.AddInParameter(dbCmd, "@as_CodigoCompania",DbType.String, pbe.CodigoCompania);
                        
                        sqlDB.AddInParameter(dbCmd, "@as_Ejercicio",DbType.String, pbe.Ejercicio);
                        
                        sqlDB.AddInParameter(dbCmd, "@a_Capital",DbType.Decimal, pbe.Capital);
                        
                        sqlDB.AddInParameter(dbCmd, "@ai_NumeroAccionistas",DbType.Int32, pbe.NumeroAccionistas);
                        
                        sqlDB.AddInParameter(dbCmd, "@a_ValorNominalUnitario",DbType.Decimal, pbe.ValorNominalUnitario);
                        
                        sqlDB.AddInParameter(dbCmd, "@ai_AccionesSuscritas",DbType.Int32, pbe.AccionesSuscritas);
                        
                        sqlDB.AddInParameter(dbCmd, "@ai_AccionesPagadas",DbType.Int32, pbe.AccionesPagadas);
                        
                        sqlDB.AddInParameter(dbCmd, "@as_Estado",DbType.String, pbe.Estado);
                        
                        sqlDB.AddInParameter(dbCmd, "@as_UsuarioModificacion",DbType.String, pbe.UsuarioModificacion);
                        
                       
                         
                                                       
                        using (IDataReader reader = sqlDB.ExecuteReader(dbCmd))
                        {
                            while (reader.Read())
                            {
                                be.CodigoCompania = DBValue.Get<string>(reader, "CodigoCompania");
                                be.FechaCreacion = DBValue.Get<DateTime>(reader, "FechaCreacion");
                                //Error
                                be.Error = DBValue.Get<Int32>(reader, "CodigoError");
                                be.Mensaje = DBValue.Get<String>(reader, "MensajeError");
                                break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        dbCmd.Dispose();
                    }
                    return be;
               }
                /// <summary>
                /// Método Actualiza un SNT_Acciones 
                /// </summary>
                /// <returns>Devuelve una entidad</returns>
             
              public BESNT_Acciones Actualizar(BESNT_Acciones pbe)
              {
                BESNT_Acciones be = new BESNT_Acciones();
                SqlDatabase sqlDB = new SqlDatabase(CadenaConexion.Obtener());
                DbCommand dbCmd = sqlDB.GetStoredProcCommand("uSP_GALEXITO_CTB_SNT_Acciones_Actualizar");
                        
                    try
                    {
                        dbCmd.CommandTimeout = CadenaConexion.CommandTimeout;

                        
                       sqlDB.AddInParameter(dbCmd,"@as_CodigoCompania",DbType.String, pbe.CodigoCompania);
                        
                       sqlDB.AddInParameter(dbCmd,"@as_Ejercicio",DbType.String, pbe.Ejercicio);
                        
                       sqlDB.AddInParameter(dbCmd,"@a_Capital",DbType.Decimal, pbe.Capital);
                        
                       sqlDB.AddInParameter(dbCmd,"@ai_NumeroAccionistas",DbType.Int32, pbe.NumeroAccionistas);
                        
                       sqlDB.AddInParameter(dbCmd,"@a_ValorNominalUnitario",DbType.Decimal, pbe.ValorNominalUnitario);
                        
                       sqlDB.AddInParameter(dbCmd,"@ai_AccionesSuscritas",DbType.Int32, pbe.AccionesSuscritas);
                        
                       sqlDB.AddInParameter(dbCmd,"@ai_AccionesPagadas",DbType.Int32, pbe.AccionesPagadas);
                        
                       sqlDB.AddInParameter(dbCmd,"@as_Estado",DbType.String, pbe.Estado);
                        
                       sqlDB.AddInParameter(dbCmd,"@as_UsuarioModificacion",DbType.String, pbe.UsuarioModificacion);
                        
                     
                         
                                                       
                        using (IDataReader reader = sqlDB.ExecuteReader(dbCmd))
                        {
                            while (reader.Read())
                            {
                                be.CodigoCompania = DBValue.Get<string>(reader, "CodigoCompania");
                                be.FechaModificacion = DBValue.Get<DateTime>(reader, "FechaModificacion");
                                //Error
                                be.Error = DBValue.Get<Int32>(reader, "CodigoError");
                                be.Mensaje = DBValue.Get<String>(reader, "MensajeError");
                                break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;                        
                    }
                    finally
                    {
                        dbCmd.Dispose();
                    }
                    return be;
               }
                                 
             
                    
                ///<summary>
                /// Método Lista datos de la tabla SNT_Acciones 
                ///</summary>
                ///<returns>Devuelve una Objeto Dataset</returns> 
                   
                 public DataSet Listar(BESNT_Acciones pbe)
                 {
                        DataSet ds = new DataSet();
                        SqlDatabase sqlDB = new SqlDatabase(CadenaConexion.Obtener());
                        DbCommand dbCmd = sqlDB.GetStoredProcCommand("uSP_GALEXITO_CTB_SNT_Acciones_Listar");
                        try
                        {
                            dbCmd.CommandTimeout = CadenaConexion.CommandTimeout;


                            ds = sqlDB.ExecuteDataSet(dbCmd);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            dbCmd.Dispose();
                        }
                        return ds;
                        
                       }
                                       
                ///<summary>
                /// Método obtiene dato de la tabla SNT_Acciones 
                ///</summary>
                ///<returns>Devuelve una Objeto  </returns> 

                 public DataSet Obtener(BESNT_Acciones pbe)
                   {
                       DataSet ds = new DataSet();
                        SqlDatabase sqlDB = new SqlDatabase(CadenaConexion.Obtener());
                        DbCommand dbCmd = sqlDB.GetStoredProcCommand("uSP_GALEXITO_CTB_SNT_Acciones_Obtener");
                        dbCmd.CommandTimeout = CadenaConexion.CommandTimeout;

                        try
                        {
                            dbCmd.CommandTimeout = CadenaConexion.CommandTimeout;

                            sqlDB.AddInParameter(dbCmd, "@as_CodigoCompania", DbType.String, pbe.CodigoCompania);
                            sqlDB.AddInParameter(dbCmd, "@as_Ejercicio", DbType.String, pbe.Ejercicio);

                            ds = sqlDB.ExecuteDataSet(dbCmd);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            dbCmd.Dispose();
                        }
                        return ds;
                        
                   }
                    
            


          

            #endregion

    }
}