using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Services
{
    public interface IDbService
    {
        
        bool AddUser(User user);
        User GetUser(User user);
        List<User> GetUsers();
        void InsertHistory(SearchHistory searchHistory);
        void InsertVideoData(VideoData videoData);
        List<SearchHistory> GetUserSearchHistory(User user);
        List<VideoData> GetUserWatchHistory(User user);
        void UpdateVideoWatchDuration(VideoData data);
    }
}
