namespace Christmas.Areas.Admin.ViewModels.Contact
{
    public class ContactVM
    {
        public int Id { get; set; }
        public string Desc { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
		public DateTime DateTime { get; set; }
        public ContactMessageVM MessageVM { get; set; }
    }
}
