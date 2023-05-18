using Microsoft.AspNetCore.Mvc.Rendering;
using RswebProject.Models;

namespace RswebProject.ViewModels
{
    public class BookPHouseEditViewModel
    {
        public Book Book { get; set; }
        public IEnumerable<int>? SelectedPHouse { get; set; }
        public IEnumerable<SelectListItem>? PHouseList { get; set; }
    }
}
