using Microsoft.AspNetCore.Components.Forms;

namespace WeGrow.Models.Requests
{
    public class ChangeImageRequest
    {
        public int ItemId { get; set; }
        public UploadedFile File { get;set;}
    }
}
