using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Conduit.ApplicationCore.DTOs.User
{
    public class UserSettingsUpdateRequestDtoRoot
    {
        public UserSettingsUpdateRequestDtoRoot(UserSettingsUpdateRequestDto userSettingsUpdateRequestDto)
        {
            UserSettingsUpdateRequestDto = userSettingsUpdateRequestDto;
        }

        [JsonProperty("user")]
        public UserSettingsUpdateRequestDto UserSettingsUpdateRequestDto { get; set; }
    }

    public class UserSettingsUpdateRequestDto
    {
        //[Required, MinLength(5)]
        public string Image { get; set; }
        public string UserName { get; set; }
        public string Bio { get; set; }
        public string Password { get; set; }
    }
}
