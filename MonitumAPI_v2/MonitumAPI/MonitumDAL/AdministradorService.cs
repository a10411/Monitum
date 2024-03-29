﻿using MonitumDAL.AuthUtils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitumDAL
{
    /// <summary>
    /// Class que visa implementar todos os métodos de Data Access Layer referentes ao Administrador
    /// Isto é, todos os acessos à base de dados relativos ao administrador estarão implementados em funções implementadas dentro desta classe
    /// </summary>
    public class AdministradorService
    {
        /// <summary>
        /// Método que visa aceder à base de dados SQL via query e confirmar se os dados de email e password são válidos e pertencem a um administrador existente na BD (efetuar Login)
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "MonitumAPI", no ficheiro appsettings.json</param>
        /// <param name="email">Email do administrador que se pretende autenticar</param>
        /// <param name="password">Password do administrador que se pretende autenticar</param>
        /// <returns>True caso dados estejam corretos (autenticação válida), false caso dados incorretos ou erro interno</returns>
        public static async Task<Boolean> LoginAdministrador(string conString, string email, string password)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    SqlCommand cmd = new SqlCommand($"SELECT * FROM Administrador where email = '{email}'", con);

                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        // Hash e salt handling
                        string hashedPWFromDb = rdr["password_hash"].ToString();
                        string salt = rdr["password_salt"].ToString();
                        rdr.Close();
                        con.Close();
                        if (HashSaltClass.CompareHashedPasswords(password, hashedPWFromDb, salt))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    rdr.Close();
                    con.Close();
                    return false;
                }
            }
            catch
            {
                throw;
            }

        }

        /// <summary>
        /// Método que visa aceder à base de dados SQL via query e adicionar um novo administrador, encriptando a sua password (registo)
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "MonitumAPI", no ficheiro appsettings.json</param>
        /// <param name="email">Email do administrador que se pretende registar</param>
        /// <param name="password">Password do administrador que se pretende registar</param>
        /// <returns>True caso administrador tenha sido introduzido, erro interno caso tenha existido algum erro</returns>
        public static async Task<Boolean> RegisterAdministrador(string conString, string email, string password)
        {
            string salt = HashSaltClass.GenerateSalt();
            byte[] hashedPW = HashSaltClass.GetHash(password, salt);
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    string addAdministrador = "INSERT INTO Administrador (email,password_hash,password_salt) VALUES (@email, @password_hash, @password_salt)";
                    using (SqlCommand queryAddAdministrador = new SqlCommand(addAdministrador))
                    {
                        queryAddAdministrador.Connection = con;
                        queryAddAdministrador.Parameters.Add("@email", SqlDbType.VarChar).Value = email;
                        queryAddAdministrador.Parameters.Add("@password_hash", SqlDbType.VarChar).Value = Convert.ToBase64String(hashedPW);
                        queryAddAdministrador.Parameters.Add("@password_salt", System.Data.SqlDbType.VarChar).Value = salt;
                        con.Open();
                        queryAddAdministrador.ExecuteNonQuery();
                        con.Close();
                        return true;
                    }

                }
            }
            catch
            {
                throw;
            }
        }
    }
}
