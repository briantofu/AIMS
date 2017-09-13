namespace AIMS.Models
{
    public interface IAccount
    {
        string Contact { get; set; }
        string Department { get; set; }
        string Email { get; set; }
        string Firstname { get; set; }
        string FullName { get; }
        string Lastname { get; set; }
        string Middlename { get; set; }
        string Roles { get; set; }
        int UserID { get; set; }
        string Username { get; set; }
    }
}