namespace CollegeLibraryDesktopApp.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Stock
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Stock()
        {
            ReservedBooks = new HashSet<ReservedBook>();
        }

        public int Id { get; set; }

        public int BookId { get; set; }

        [Required]
        [StringLength(150)]
        public string Comment { get; set; }

        public virtual Book Book { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReservedBook> ReservedBooks { get; set; }
    }
}
