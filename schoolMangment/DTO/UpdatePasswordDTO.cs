namespace schoolMangment.DTO
{
    public class UpdatePasswordDTO
    {
        public int StudentId { get; set; }      // The ID of the student/user whose password is being updated
        public string OldPassword { get; set; }     // The current password for verification
        public string NewPassword { get; set; }     // The new password to be set
    }
    public class UpdateEmailDTO
    {
        public int StudentId { get; set; }   // The ID of the student/user whose email is being updated
        public string NewEmail { get; set; }     // The new email address to be set
        public string ConfirmationEmail { get; set; } // A field to confirm the new email address
    }


}
