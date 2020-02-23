using System;

namespace Application.Authentication
{
    public class TokenDto
    {
        public string Token { get; set; }

        public string UserName { get; set; }

        public int Id { get; set; }

        public DateTime ExpDate { get; set; }
    }
}
