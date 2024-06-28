namespace CollegeLibraryDesktopApp.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ReservedBook
    {
        public int Id { get; set; }

        public int StockId { get; set; }

        public int UserId { get; set; }
        [NotMapped]
        public string UserName { get { return User.Surname + " " + User.Name[0] + ". " + User.Patronymic[0]+"."; } set { } }
        [NotMapped]
        public string RecordNumber { get { return User.RecordNumber.ToString(); } set { } }
        [NotMapped]
        public string Period { get { return "c "+DateAdd.ToString("dd.MM.yyyy")+" до "+DateAdd.AddMonths(1).ToString("dd.MM.yyyy"); } set { } }
        [NotMapped]
        public string BookName { get { return Stock.Book.Title.ToString(); } set { } }
        public DateTime DateAdd { get; set; }

        public DateTime? DateEnd { get; set; }

        public virtual Stock Stock { get; set; }

        public virtual User User { get; set; }
        [NotMapped]
        public DateTime DateIssued  { get{ return DateAdd.AddMonths(1); } }
        [NotMapped]
        public string Count
        {
            get
            {
                int data = Convert.ToInt32((DateAdd - DateTime.Now).TotalDays);
                if (data > 0)
                {
                    return "Осталось: " +
                        Convert.ToInt32(data)
                        .ToString() + "д.";
                }
                else
                {
                    return "Просрочено на " +
                        Convert.ToInt32(data)
                        .ToString().Trim('-') + "д.";
                }
            }
        }
    }
}
