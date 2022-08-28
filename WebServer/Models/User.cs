using System.Text.Json.Serialization;
namespace WebServer.Models

{
    public class User
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? Password { get; set; }
        public int? Tg_id { get; set; }

        public int? Code { get; set; }
       
        
    } 
}

