using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.YouTube.v3.Data;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Services;
using WebAPI.YoutubeAPI;

namespace WebAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //private Context _context;
        private  readonly IDbService _service;

        public UserController(IDbService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("InsertUser")]
        public bool Insert([FromBody]User user)
        {
            return _service.AddUser(user);
        }

        [HttpPost]
        [Route("InsertVideoData")]
        public void InsertVideoData([FromBody]VideoData data)
        {
            _service.InsertVideoData(data);
        }

        [HttpPost]
        [Route("UpdateVideoWatchDuration")]
        public void UpdateVideoWatchDuration([FromBody]VideoData data)
        {
            _service.UpdateVideoWatchDuration(data);
        }
        
        [HttpPost]
        [Route("GetUser")]
        public User GetUser([FromBody]User user)
        {
            return _service.GetUser(user);
        }

        [HttpPost]
        [Route("GetVideos")]
        public async Task<List<SearchResult>> GetVideos([FromBody]SearchHistory searchHistory)
        {
            var ya = new YoutubeApi();
            await ya.Run(searchHistory.SearchTerm);
            _service.InsertHistory(searchHistory);
            return ya.Videos;
        }

        [HttpGet]
        [Route("GetUsers")]
        public ActionResult<List<User>> Getusers()
        {
            return _service.GetUsers();
        }

        [HttpPost]
        [Route("GetUserSearchHistory")]
        public List<SearchHistory> GetUserSearchHistory([FromBody]User user)
        {
            return _service.GetUserSearchHistory(user);
        }

        [HttpPost]
        [Route("GetUserWatchHistory")]
        public List<VideoData> GetUserWatchHistory([FromBody]User user)
        {
            return _service.GetUserWatchHistory(user);
        }
    }
}