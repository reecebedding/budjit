using System.ComponentModel.DataAnnotations;

namespace budjit.ui.API.ViewModel
{
    public class TagViewModel
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
    }
}