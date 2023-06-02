using Microsoft.AspNetCore.Mvc;
using static DataAccess.SD;

namespace OnlineCourse_Web1.Models
{
    public class APIRequest
    {
        public ApiType ApiType { get; set; } = ApiType.GET;

        public string Url { get; set; }

        public object Data { get; set; }
    }
}
