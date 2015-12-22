using MySql.Data.MySqlClient;
using ProductApp.JsonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductApp
{
    public class SearchStringRep
    {
        

        public IEnumerable<Post> GetAll(string searchString)
        {
            var sql = string.Format(
                    "SELECT qi_ID, title, body, comment, viewcount, votes FROM Post where body like '%{0}%'",searchString);
            foreach (var post in ExecuteQuery(sql))
                yield return post;
        }

        private static IEnumerable<Post> ExecuteQuery(string sql)
        {
            using (var connection = new MySqlConnection("server=wt-220.ruc.dk;database=raw2;uid=raw2;pwd=raw2;"))
            {
                connection.Open();
                var cmd = new MySqlCommand(sql, connection);
                using (var reader = cmd.ExecuteReader())
                {
                    int idtemp = 0;
                    string titletemp = "No title available";
                    string bodytemp = "This post has no body";
                    string commenttemp = "No comments to this post";
                    int viewcounttemp = 0;
                    int votestemp = 0;
                    while (reader.HasRows && reader.Read())
                    {

                        if (!reader.IsDBNull(0))
                        {
                            idtemp = reader.GetInt32(0);
                        }
                        else if (reader.IsDBNull(0))
                        {
                            idtemp = 0;
                        }
                        if (!reader.IsDBNull(1))
                        {
                            titletemp = reader.GetString(1);
                        }
                        else if (reader.IsDBNull(1))
                        {
                            titletemp = "No title available";
                        }
                        if (!reader.IsDBNull(2))
                        {
                            bodytemp = reader.GetString(2);
                        }
                        else if (reader.IsDBNull(2))
                        {
                            bodytemp = "No body for this post";
                        }
                        if (!reader.IsDBNull(3))
                        {
                            commenttemp = reader.GetString(3);
                        }
                        else if (reader.IsDBNull(3))
                        {
                            commenttemp = "No comments to this post";
                        }
                        if (!reader.IsDBNull(4))
                        {
                            viewcounttemp = reader.GetInt32(4);
                        }
                        else if (reader.IsDBNull(4))
                        {
                            viewcounttemp = 0;
                        }
                        if (!reader.IsDBNull(5))
                        {
                            votestemp = reader.GetInt32(5);
                        }
                        else if (reader.IsDBNull(5))
                        {
                            votestemp = 0;
                        }

                        yield return new Post
                        {
                            _Qi_ID = idtemp,
                            _title = titletemp,
                            _body = bodytemp,
                            _comment = commenttemp,
                            _viewCount = viewcounttemp,
                            _votes = votestemp,
                        };

                    }
                }
            }
        }
    }
}