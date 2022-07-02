using AspNetCrudDapper.Entidades;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace AspNetCrudDapper.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        IConfiguration _configuration;

        public ProdutoRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConnection()
        {
            var connection = _configuration.GetSection("ConnectionStrings").
                GetSection("ProdutoConnection").Value;
            return connection;
        }

        public int Add(Produto produto)
        {
            var connectionString = this.GetConnection();
            int count = 0;

            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var qry = @"INSERT INTO Produtos(Nome, Estoque, Preco) VALUES (@Nome, @Estoque, @Preco);
                              SELECT CAST(SCOPE_IDENTITY() as INT)";
                    count = con.Execute(qry, produto);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }

                return count;
            }

        }

        public int Delete(int id)
        {
            var connectionString = this.GetConnection();
            int count = 0;

            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var qry = $"DELETE FROM Produtos WHERE ProdutoId = {id}";
                    count = con.Execute(qry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally { con.Close(); }
                return count;
            }

        }

        public int Edit(Produto produto)
        {
            var connStr = this.GetConnection();
            int count = 0;

            using(var con = new SqlConnection(connStr))
            {
                try
                {
                    con.Open();
                    var qry = @"UPDATE Produtos
                              SET Nome = @Nome, Estoque = @Estoque, Preco = @Preco"+
                              $"WHERE ProdutoId = {produto.ProdutoId}";
                    count = con.Execute(qry, produto);

                } catch(Exception ex)
                {
                    throw ex;
                } finally { con.Close(); }
                return count;
            }
        }

        public Produto Get(int id)
        {
            var connStr = this.GetConnection();
            Produto produto = new Produto();

            using(var con = new SqlConnection(connStr))
            {
                try
                {
                    con.Open();
                    var qry = $"SELECT * FROM Produtos WHERE ProdutoId = {id}";
                    produto = con.Query<Produto>(qry).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally { con.Close(); }
                return produto;
            }
            
        }

        public List<Produto> GetProdutos()
        {
            var connStr = this.GetConnection();
            var produtos = new List<Produto>();

            using (var con = new SqlConnection(connStr))
            {
                try
                {
                    con.Open();
                    var qry = $"SELECT * FROM Produtos";
                    produtos = con.Query<Produto>(qry).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally { con.Close(); }
                return produtos;
            }
        }
    }
}
