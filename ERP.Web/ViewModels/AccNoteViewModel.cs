using System.ComponentModel.DataAnnotations;

namespace ERP.Web.ViewModels
{
    public class AccNoteViewModel:BaseModel
    {
        public int NoteID { get; set; }

        [Display(Name = "Note No")]
        public int NoteNo { get; set; }

        [Display(Name = "Note Name")]
        public string NoteName { get; set; }
        public int SlNo { get; set; }

    }
}