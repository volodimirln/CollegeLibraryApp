namespace CollegeLibraryDesktopApp.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AuthorsInBook")]
    public partial class AuthorsInBook
    {
        public int Id { get; set; }

        public int AuthorId { get; set; }

        public int BookId { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateAdd { get; set; }

        public virtual Author Author { get; set; }

        public virtual Book Book { get; set; }
    }
}
