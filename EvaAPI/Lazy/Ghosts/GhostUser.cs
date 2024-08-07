using EvaAPI.Entities;
using EvaAPI.Lazy.Proxies;

namespace EvaAPI.Lazy.Ghosts;

public class GhostUser : UserProxy
{
    private Func<ApplicationUser> load;

    private LoadStatus status;

    public bool IsGhost => status == LoadStatus.GHOST;
    public bool IsLoaded => status == LoadStatus.LOADED;

    public override int Id
    {
        get
        {
            Load();
            return base.Id;
        }
        set
        {
            Load();
            base.Id = value;
        }
    }

    public override string UserName
    {
        get
        {
            Load();
            return base.UserName;
        }
        set
        {
            Load();
            base.UserName = value;
        }
    }

    public override string FirstName
    {
        get
        { 
            Load();
            return base.FirstName;
        }
        set
        { 
            Load();
            base.FirstName = value;
        }
    }
    
    public override string LastName
    {
        get
        { 
            Load();
            return base.LastName;
        }
        set
        {
            Load();
            base.LastName = value;
        }
    }

    public override string Email
    {
        get
        { 
            Load();
            return base.Email;
        }
        set
        {
            Load();
            base.Email = value;
        }
    }
    
    public override string Password
    {
        get
        { 
            Load();
            return base.Password;
        }
        set
        { 
            Load();
            base.Password = value;
        }
    }

    public GhostUser(Func<ApplicationUser> load)
    {
        this.load = load;
        this.status = LoadStatus.GHOST;
    }


    private void Load()
    {
        if (IsGhost)
        {
            status = LoadStatus.LOADING;

            var user = load();
            base.Id = user.Id;
            base.FirstName = user.FirstName;
            base.LastName = user.LastName;
            base.Password = user.Password;
            base.Email = user.Email;
            base.UserName = user.UserName;

            status = LoadStatus.LOADED;
        }
    }
}