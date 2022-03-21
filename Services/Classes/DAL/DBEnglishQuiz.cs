using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using EnglishQuizTelegramBot.Models;
using EnglishQuizTelegramBot.Tools;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EnglishQuizTelegramBot.Services.Classes.DAL
{
    public class DBEnglishQuiz : IDBEnqlishQuiz
    {
        private SqlConnection _connection;
        private readonly string _englishQuizConnectionString;
        private readonly ILogger<DBEnglishQuiz> _logger;

        public DBEnglishQuiz(IOptions<ConnectionStrings> options, ILogger<DBEnglishQuiz> logger)
        {
            _logger = logger;
            _englishQuizConnectionString = options.Value.EnglishQuiz;
        }

        public async Task<int> proc_AddMember(Member member)
        {
            return await Task.Run(() =>
            {
                _connection = new SqlConnection(_englishQuizConnectionString);
                SqlCommand command = new SqlCommand("[dbo].[proc_AddMember]", _connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@xmlData", ModelToXml(member)));
                command.Parameters.Add(new SqlParameter("@xmlResult", SqlDbType.Int));
                command.Parameters["@xmlResult"].Direction = ParameterDirection.Output;

                try
                {
                    _connection.Open();
                    command.ExecuteNonQuery();
                    return 0;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                }
                finally
                {
                    _connection.Close();
                }
                return -1;
            });
        }

        public async Task<int> proc_AddWord(Word word)
        {
            return await Task.Run(() =>
            {
                _connection = new SqlConnection(_englishQuizConnectionString);
                SqlCommand command = new SqlCommand("[dbo].[proc_AddWord]", _connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@xmlData", ModelToXml(word)));
                command.Parameters.Add(new SqlParameter("@xmlResult", SqlDbType.Int));
                command.Parameters["@xmlResult"].Direction = ParameterDirection.Output;

                try
                {
                    _connection.Open();
                    command.ExecuteNonQuery();
                    return Convert.ToInt32(command.Parameters["@xmlResult"].Value.ToString());
                }
                catch (Exception e)
                {
                    _logger.LogError(e, e.Message);
                }
                finally
                {
                    _connection.Close();
                }
                return -1;
            });
        }

        public async Task<List<Word>> proc_GetWordsForGame()
        {
            return await Task.Run(() =>
            {
                _connection = new SqlConnection(_englishQuizConnectionString);
                SqlCommand command = new SqlCommand("[dbo].[proc_GetWordsForGame]", _connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@xmlResult", SqlDbType.Xml));
                command.Parameters["@xmlResult"].Direction = ParameterDirection.Output;

                try
                {
                    _connection.Open();
                    command.ExecuteNonQuery();
                    return XmlToModel<Words>(command.Parameters["@xmlResult"].Value.ToString()).AllWords;
                }
                catch (Exception e)
                {
                    _logger.LogError(e, e.Message);
                }
                return new List<Word>();
            });
        }

        public Task<int> proc_UpdateScore(long memberId)
        {
            return Task.Run(() =>
            {
                
                _connection = new SqlConnection(_englishQuizConnectionString);
                SqlCommand command = new SqlCommand("[dbo].[proc_UpdateScore]", _connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@xmlData", memberId));

                try
                {
                    _connection.Open();
                    command.ExecuteNonQuery();
                    return 0;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                }
                
                return 0;
            });
        }

        public Task<ScoreInfo> proc_GetScore(long memberId)
        {
            return Task.Run(() =>
            {
                _connection = new SqlConnection(_englishQuizConnectionString);
                SqlCommand command = new SqlCommand("[dbo].[proc_GetScore]", _connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@xmlData", memberId));
                command.Parameters.Add(new SqlParameter("@xmlResult", SqlDbType.Xml));
                command.Parameters["@xmlResult"].Direction = ParameterDirection.Output;

                try
                {
                    _connection.Open();
                    command.ExecuteNonQuery();
                    return XmlToModel<ScoreInfo>(command.Parameters["@xmlResult"].Value.ToString());
                }
                catch (Exception e)
                {
                    _logger.LogError(e, e.Message);
                }
                
                return new ScoreInfo();
            });
        }

        private string ModelToXml<T>(T model)
        {
            using (var sw = new StringWriter())
            {
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(sw, model);
                return sw.ToString();
            }
        }

        private T XmlToModel<T>(string xml)
        {
            using (StringReader reader = new StringReader(xml))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(reader);
            }
        }
    }
}