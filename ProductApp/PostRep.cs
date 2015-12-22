using ProductApp.JsonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace ProductApp
{
    public class PostRep
    {
        string connectionString = "server=wt-220.ruc.dk;database=raw2;uid=raw2;pwd=raw2;";

        public IEnumerable<Post> GetAll(int limit, int offset)
        {
            var sql = string.Format(
                    "SELECT qi_ID, title, body, comment, viewcount, votes FROM Post limit {0} offset {1}",
                    limit, offset);
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

        public Post find(int id)
        {
            var sql = string.Format("SELECT qi_ID, title, body, comment, viewcount, votes FROM Post WHERE qi_ID = {0}", id);
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (var cmd = new MySqlCommand(sql))
                {
                    cmd.Connection = connection;

                    int idtemp = 0;
                    string titletemp = "No title available";
                    string bodytemp = "This post has no body";
                    string commenttemp = "No comments to this post";
                    int viewcounttemp = 0;
                    int votestemp = 0;
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            if (!reader.IsDBNull(0)){
                                idtemp = reader.GetInt32(0);
                            }
                            else if (reader.IsDBNull(0)){
                                idtemp = 0;
                            }
                            if (!reader.IsDBNull(1)){
                                titletemp = reader.GetString(1);
                            }
                            else if (reader.IsDBNull(1)){
                                titletemp = "No title available";
                            }
                            if (!reader.IsDBNull(2))
                            {
                                bodytemp = reader.GetString(2);
                            }
                            else if (reader.IsDBNull(2))
                            {
                                bodytemp = "This post has no body";
                            }
                            if (!reader.IsDBNull(3))
                            {
                                commenttemp = reader.GetString(3);
                            }
                            else if (reader.IsDBNull(3))
                            {
                                commenttemp = "There is no comments to this post";
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
                            
                            var post = new Post()
                            {
                                _Qi_ID = idtemp,
                                _title = titletemp,
                                _body = bodytemp,
                                _comment = commenttemp,
                                _viewCount = viewcounttemp,
                                _votes = votestemp,
                            };
                            return post;
                        }
                    }
                }
                return null;
            }
        }
        public Post Post(string title, string body)
        {
            int maxID =0;
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string insert = "insert into Post ( qi_ID, title, body,  viewcount,owner_id, votes)  values (@qaid, @title, @body, 0, 1, 0)";
                
                MySqlCommand cmd = new MySqlCommand(insert);
                cmd.Connection = connection;
                
                var sql = string.Format("SELECT max(qi_id) from Post");
                MySqlCommand cmdd = new MySqlCommand(sql);
                cmdd.Connection = connection;
                using (var reader = cmdd.ExecuteReader())
                {
                    if (reader.HasRows && reader.Read())
                    {
                         maxID = reader.GetInt32(0)+1;
                    }
                }
                cmd.Parameters.AddWithValue("@body", body);
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@qaid", maxID);
                cmd.ExecuteNonQuery();
            }
            return find(maxID); 
        }

        public Post find(string body)
        {
            var sql = string.Format("SELECT qi_ID, title, body, viewcount, votes FROM Post WHERE body like {0}", "\""+body+"\"");
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (var cmd = new MySqlCommand(sql))
                {
                    cmd.Connection = connection;
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            var post = new Post()
                            {
                                _Qi_ID = reader.GetInt32(0),
                                _title = reader.GetString(1),
                                _body = reader.GetString(2),
                                _viewCount = reader.GetInt32(3),
                                _votes = reader.GetInt32(4),
                            };
                            return post;
                        }
                    }
                }
                return null;
            }
        }
    }
}

