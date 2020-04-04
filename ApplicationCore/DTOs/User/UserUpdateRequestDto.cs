using System.Text.Json.Serialization;

namespace Conduit.ApplicationCore.DTOs.User
{
    public class UserSettingsUpdateRequestDtoRoot
    {
        [JsonPropertyName("user")]
        public UserUpdateRequestDto UserSettingsUpdateRequestDto { get; set; }
    }

    public class UserUpdateRequestDto
    {
        [JsonPropertyName("image")]
        public string ImageUrl { get; set; }
        public string UserName { get; set; }
        public string Bio { get; set; }
        public string Password { get; set; }
    }
}
