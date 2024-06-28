namespace CollegeLibraryDesktopApp.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Session
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime? DateEnd { get; set; }

        public virtual User User { get; set; }
    }
}
