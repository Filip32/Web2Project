using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class PictureModel
    {
        private string id;
        private string picture;

        public PictureModel()
        {

        }

        public string Id { get { return id; } set { id = value; } }
        public string Picture { get { return picture; } set { picture = value; } }
    }
}