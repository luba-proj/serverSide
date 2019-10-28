using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.YoutubeAPI;

namespace WebAPI.Services
{
    public class DbService : IDbService
    {

        private static readonly string _conn = "Server=tcp:luba.database.windows.net,1433;Initial Catalog=Youtubedb;Persist Security Info=False;User ID=luba;Password=Lz317317;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
       //private static readonly string _conn = "Server=DESKTOP-CC4Q17M; Database=Youtube;Trusted_Connection=True;MultipleActiveResultSets=true";
        //private readonly SqlConnection sqlCon = new SqlConnection(_conn);
        public DbService()
        {
            //sqlCon.Open();
        }
        public bool AddUser(User user)
        {

           using(SqlConnection sqlCon = new SqlConnection(_conn))
            {
                SqlCommand sqlCmd = new SqlCommand("InsertUser", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@name", user.Name);
                sqlCmd.Parameters.AddWithValue("@lastName", user.LastName);
                sqlCmd.Parameters.AddWithValue("@phoneNumber", user.PhoneNumber);
                sqlCmd.Parameters.AddWithValue("@userName", user.UserName);
                sqlCmd.Parameters.AddWithValue("@password", user.Password);
                sqlCmd.Parameters.Add("@isSucsess", SqlDbType.Int);
                sqlCmd.Parameters["@isSucsess"].Direction = ParameterDirection.Output;
                sqlCon.Open();
                sqlCmd.ExecuteNonQuery(); 
                var res = sqlCmd.Parameters["@isSucsess"].Value;
                sqlCon.Close();
                if ((int)res == 1)
                    return true;
                else
                    return false;
            }
            
                
            
        }
        public void InsertHistory(SearchHistory searchHistory)
        {
            using (SqlConnection sqlCon = new SqlConnection(_conn))
            {
                SqlCommand sqlCmd = new SqlCommand("InsertHistory", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@userId", searchHistory.UserId);
                sqlCmd.Parameters.AddWithValue("@searchHistory", searchHistory.SearchTerm);
                sqlCon.Open();
                sqlCmd.ExecuteNonQuery();
                sqlCon.Close();
            }
          
          
        }
        public void InsertVideoData(VideoData videoData)
        {
            using (SqlConnection sqlCon = new SqlConnection(_conn))
            {
                SqlCommand sqlCmd = new SqlCommand("InsertVideoDetails", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@userId", videoData.UserId);
                sqlCmd.Parameters.AddWithValue("@videoUrl", videoData.VideoURL);
                // sqlCmd.Parameters.AddWithValue("@duration", videoData.Duration);
                sqlCon.Open();
                sqlCmd.ExecuteNonQuery();
                sqlCon.Close();
            }

        }
        public void UpdateVideoWatchDuration(VideoData data)
        {
            using (SqlConnection sqlCon = new SqlConnection(_conn))
            {
                SqlCommand sqlCmd = new SqlCommand("UpdateVideoWatchDuration", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@userId", data.UserId);
                sqlCmd.Parameters.AddWithValue("@videoUrl", data.VideoURL);
                sqlCmd.Parameters.AddWithValue("@duration", data.Duration);
                sqlCon.Open();
                sqlCmd.ExecuteNonQuery();
                sqlCon.Close();
            }
        }
        public User GetUser(User user)
        {
            User user1 = new User();
            using (SqlConnection sqlCon = new SqlConnection(_conn))
            {
                SqlCommand sqlCmd = new SqlCommand("GetUser", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@password", user.Password);
                sqlCmd.Parameters.AddWithValue("@userName", user.UserName);
                sqlCon.Open();
                var rdr = sqlCmd.ExecuteReader();

                if (rdr.Read())
                {

                    user1.Id = (long)Convert.ToDouble(rdr["Id"].ToString());
                    user1.Name = rdr["name"].ToString();
                    user1.LastName = rdr["lastName"].ToString();
                    user1.PhoneNumber = rdr["phoneNumber"].ToString();
                    user1.Password = rdr["password"].ToString();
                    user1.UserName = rdr["userName"].ToString();
                    user1.Permissions = Convert.ToBoolean(rdr["permissions"]);
                }
                sqlCon.Close();
            }

            return user1;
        }
        public List<User> GetUsers()
        {
            List<User> users = new List<User>();
            using (SqlConnection sqlCon = new SqlConnection(_conn))
            {
                SqlCommand sqlCmd = new SqlCommand("GetAllUsers", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCon.Open();
                var rdr = sqlCmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    var dt = new DataTable();
                    dt.Load(rdr);
                    foreach (DataRow row in dt.Rows)
                    {
                        User user = new User();
                        user.Id = (long)Convert.ToDouble(row["Id"].ToString());
                        user.Name = row["name"].ToString();
                        user.LastName = row["lastName"].ToString();
                        user.PhoneNumber = row["phoneNumber"].ToString();
                        user.Password = row["password"].ToString();
                        user.UserName = row["userName"].ToString();
                        user.Permissions = Convert.ToBoolean(row["permissions"]);
                        users.Add(user);
                    }


                }
                sqlCon.Close();
            }
                
          
            return users;
        }
        public List<SearchHistory> GetUserSearchHistory(User user)
        {
            List<SearchHistory> searchHistoryList = new List<SearchHistory>();
            using (SqlConnection sqlCon = new SqlConnection(_conn))
            {
             
                SqlCommand sqlCmd = new SqlCommand("GetHistory", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@userId", user.Id);
                sqlCon.Open();
                var rdr = sqlCmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    var dt = new DataTable();
                    dt.Load(rdr);
                    foreach (DataRow row in dt.Rows)
                    {
                        SearchHistory history = new SearchHistory();
                        history.Id = (long)Convert.ToDouble(row["Id"].ToString());
                        history.UserId = (long)Convert.ToDouble(row["userId"].ToString());
                        history.SearchTerm = row["searchHistory"].ToString();

                        searchHistoryList.Add(history);
                    }
                }
                sqlCon.Close();
            }

               
           
            return searchHistoryList;
        }
        public List<VideoData> GetUserWatchHistory(User user)
        {
            VideoData watchHistory = new VideoData();
            List<VideoData> watchHistoryList = new List<VideoData>();
            using (SqlConnection sqlCon = new SqlConnection(_conn))
            {
               
                SqlCommand sqlCmd = new SqlCommand("GetVideoData", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@userId", user.Id);
                sqlCon.Open();
                var rdr = sqlCmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    var dt = new DataTable();
                    dt.Load(rdr);
                    foreach (DataRow row in dt.Rows)
                    {
                        VideoData watchHistory1 = new VideoData();
                        watchHistory1.Id = (long)Convert.ToDouble(row["Id"].ToString());
                        watchHistory1.UserId = (long)Convert.ToDouble(row["userId"].ToString());
                        watchHistory1.VideoURL = row["videoUrl"].ToString();
                        try
                        {
                            watchHistory1.Duration = Convert.ToDecimal(row["duration"]);
                        }
                        catch (Exception e)
                        {
                            watchHistory1.Duration = 0;
                        }

                        watchHistoryList.Add(watchHistory1);
                    }
                }
                sqlCon.Close();
            }

               
            return watchHistoryList;
        }

        

        // Destructor
        ~DbService()
        {
            //sqlCon.Close();
        }
    }
}
