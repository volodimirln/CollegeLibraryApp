namespace CollegeLibraryDesktopApp.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.IO;
    using System.Windows.Media.Imaging;

    [Table("PublishingCompany")]
    public partial class PublishingCompany
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PublishingCompany()
        {
            Books = new HashSet<Book>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(150)]
        public string Description { get; set; }

        [Required]
        [StringLength(150)]
        public string Genre { get; set; }

        public byte[] Logo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Book> Books { get; set; }

        [NotMapped]
        public  BitmapImage bitmap { get { return ConvertToBipmapImage(Logo); } }

        private BitmapImage ConvertToBipmapImage(byte[] array)
        {
            using (var ms = new MemoryStream(array))
            {
                var images = new BitmapImage();
                images.BeginInit();
                images.CacheOption = BitmapCacheOption.OnLoad;
                images.StreamSource = ms;
                images.EndInit();
                return images;
            }
        }
    }
}
