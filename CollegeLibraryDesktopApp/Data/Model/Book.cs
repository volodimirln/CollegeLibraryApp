namespace CollegeLibraryDesktopApp.Data.Model
{
    using CollegeLibraryDesktopApp.Services;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Windows.Documents;
    using System.Windows.Media.Imaging;

    public partial class Book
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Book()
        {
            AuthorsInBooks = new HashSet<AuthorsInBook>();
            Stocks = new HashSet<Stock>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; }

        public int GenreId { get; set; }

        public DateTime DateAdd { get; set; }

        public bool Status { get; set; }

        public int? PublishingId { get; set; }

        [NotMapped]
        public string DataAddStr { get { return "Дата добавления: "+ DateAdd.ToString("dd.MM.yyyy"); } set { } }

        [NotMapped]
        public string ShortTitle { get { return new string(Title.Take(35).ToArray()); } }
        public byte[] CoverImage { get; set; }
        [NotMapped]
        public BitmapImage bipmap { get { return ToImage(CoverImage); } }
        [NotMapped]
        public string Authors { get 
            { 
                string data = string.Join(", ", AuthorsInBooks.Select(r => r.Author.Name +" "+ r.Author.Surname));
                if (data.Length > 30)
                {
                    return data.Substring(0, 30).ToString()+"...";
                }
                else
                {
                    return data;
                }
            } 
        }

        [NotMapped]
        public List<Stock> CopiesBook { get { return Model.GetContext().Stocks.Where(p => p.BookId == this.Id).ToList(); } }

        [NotMapped]
        public int AllCopies { get { return CopiesBook.Count;} set { }  }

        [NotMapped]
        public List<ReservedBook> IssuedBooksList { get { 
                return Model.GetContext().ReservedBooks.Where(p => p.Stock.Book.Id == this.Id && p.DateEnd == null ).OrderByDescending(p => p.Id).ToList(); } set { } }

        [NotMapped]
        public int IssuedBooks { get { return 
                    IssuedBooksList.Count; 
            } set { } }
        [NotMapped]
        public int Remainder { get { return new Statistics().GetAllIssuedStatistics(CopiesBook.Count, IssuedBooks)[2]; } set { } }


        [Required]
        [StringLength(150)]
        public string ISBNCode { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AuthorsInBook> AuthorsInBooks { get; set; }

        public virtual Genre Genre { get; set; }

        public virtual PublishingCompany PublishingCompany { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Stock> Stocks { get; set; }
        public BitmapImage ToImage(byte[] array)
        {
            if (array != null)
            {
                using (var ms = new System.IO.MemoryStream(array))
                {
                    var images = new BitmapImage();
                    images.BeginInit();
                    images.CacheOption = BitmapCacheOption.OnLoad;
                    images.StreamSource = ms;
                    images.EndInit();
                    images.Freeze();
                    return images;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
