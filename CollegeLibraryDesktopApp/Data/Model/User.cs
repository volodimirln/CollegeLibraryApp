namespace CollegeLibraryDesktopApp.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            ReservedBooks = new HashSet<ReservedBook>();
            Sessions = new HashSet<Session>();
        }

        [NotMapped]
        public string ShortName { get { return Surname + " " + Name[0] + ". " + Patronymic[0] + "."; } set { } }
        public int Id { get; set; }
        [NotMapped]
        public string FullName { get { return Surname + " " + Name + " " + Patronymic; } set { } }

        [NotMapped]
        public int IssuedEndBooksCount { get { return Model.GetContext().ReservedBooks.Where(p => p.UserId == Id && p.DateEnd != null).Count(); } set { } }
        [NotMapped]
        public int IssuedBooksCount { get { return Model.GetContext().ReservedBooks.Where(p => p.UserId == Id && p.DateEnd == null).Count(); } set { } }
        [Required]
        [StringLength(70)]
        public string Name { get; set; }

        [Required]
        [StringLength(150)]
        public string Surname { get; set; }

        [Required]
        [StringLength(70)]
        public string Patronymic { get; set; }

        [Required]
        [StringLength(11)]
        public string Phone { get; set; }

        [Required]
        [StringLength(70)]
        public string Email { get; set; }

        [StringLength(32)]
        public string HashPassword { get; set; }

        public bool Status { get; set; }

        public DateTime DateRegistrseion { get; set; }

        [NotMapped]
        public string DateRegistrseionStr { get { return DateRegistrseion.ToString("dd.MM.yyyy"); } set { } }

        public DateTime? DateChange { get; set; }

        public int? RecordNumber { get; set; }

        public int RoleId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReservedBook> ReservedBooks { get; set; }

        public virtual Role Role { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Session> Sessions { get; set; }
    }
}
