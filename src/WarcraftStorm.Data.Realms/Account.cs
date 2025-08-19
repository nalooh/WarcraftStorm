using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;

namespace WarcraftStorm.Data.Realms;

[Table("Accounts", Schema = "Realms")]
public class Account
{
    [Key, Column("AccountID")]
    public int Id { get; set; }

    [Column("AccountUsername")]
    public string UserName { get; set; }

    [Column("AccountPassword")]
    public byte[]? Password { get; set; }

    [Column("AccountIsLocked")]
    public bool IsLocked { get; set; } = false;

    [Column("AccountSalt")]
    public byte[]? Salt { get; set; }

    [Column("AccountVerifier")]
    public byte[]? Verifier { get; set; }

    [Column("AccountSessionKey")]
    public byte[]? SessionKey { get; set; }

    public void SetUserPassword(string password)
    {
        Password = SHA1.HashData(Encoding.ASCII.GetBytes($"{UserName.ToUpper()}:{password.ToUpper()}"));
    }
    
}
