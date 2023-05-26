using EmptyStock.Domain.Models.Identity;

namespace EmptyStock.Mvc.Models;

public class EditUserViewModel
{
    private string _role;

    public EditUserViewModel() { }
    public EditUserViewModel(StockUser stockUser, string role)
    {
        UserId = stockUser.Id;
        Username = stockUser.UserName;
    }
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Role 
    { 
        get => _role; 
        set
        {
            _role = IdentityHelpers.GetRole(value);
        }
    }

}
