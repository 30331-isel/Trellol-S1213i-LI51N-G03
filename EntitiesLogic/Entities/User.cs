using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLogic.Entities
{
    public class User
    {
        private IList<Board> _boards = new List<Board>();

        public string Username { get; set; }

        public string Name { get; set; }

        public string ImageProfile { get; set; }

        public string ImageMimeType { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }

        //public string[] Roles { get; set; }

        public bool isConfirmed { get; set; }

        public IList<Board> Boards { get { return _boards; } }
    }
}
