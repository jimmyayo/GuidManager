using System;
using API.Helpers;
using API.Models;

public interface IGuidRepository
{
    IEnumerable<MyGuid> GetAll();
    IEnumerable<MyGuid> GetAllValid();
    MyGuid Get(string guid);
    MyGuid Add(MyGuid guid);
    void Remove(string guid);
    MyGuid Update(MyGuid guid);
}

public class GuidRepository : IGuidRepository
{
    
    private List<MyGuid> _guidList;

    public GuidRepository()
    {
        // Initialize some seed data for easier testing
        _guidList = new List<MyGuid>();
        Add(new MyGuid {
            Guid = GuidHelper.GenerateNewGuid(),
            User = "John Doe",
            Expire = DateTime.Now.AddDays(30).ToUniversalTime()
        });

        Add(new MyGuid {
            Guid = GuidHelper.GenerateNewGuid(),
            User = "Jane Smith",
            Expire = DateTime.Now.AddDays(10).ToUniversalTime()
        });
        
        Add(new MyGuid {
            Guid = GuidHelper.GenerateNewGuid(),
            User = "Aero Smith",
            Expire = DateTime.Now.AddDays(1).ToUniversalTime()
        });

        Add(new MyGuid {
            Guid = GuidHelper.GenerateNewGuid(),
            User = "Who Dat",
            Expire = DateTime.Now.AddDays(10).ToUniversalTime()
        });
    }
    public MyGuid Add(MyGuid myGuid)
    {
        var duplicateExists = _guidList.Exists(g => g.Guid == myGuid.Guid);
        
        // If no guid provided, or provided guid is invalid, or same guid already exists - generate our own
        if (string.IsNullOrWhiteSpace(myGuid.Guid) || !GuidHelper.IsValidGuid(myGuid.Guid) || duplicateExists)
            myGuid.Guid = GuidHelper.GenerateNewGuid();

        // if no expiration date was provided, generate default of 30 days in future
        myGuid.Expire ??= DateTime.UtcNow.AddDays(30);

        _guidList.Add(myGuid);
        return myGuid;
    }

    public MyGuid Get(string guid)
    {
        return _guidList.SingleOrDefault(g => g.Guid == guid);
    }

    public IEnumerable<MyGuid> GetAll()
    {
        return _guidList;
    }

    IEnumerable<MyGuid> IGuidRepository.GetAllValid()
    {
        return _guidList.Where(g => g.Expire > DateTime.UtcNow);
    }

    public void Remove(string guid)
    {
        _guidList.Remove(_guidList.First( g => g.Guid == guid));
    }

    
    public MyGuid Update(MyGuid myGuid)
    {
        //Validation checks on update
        if (myGuid == null) throw new ArgumentNullException(nameof(myGuid));
        
        if (string.IsNullOrWhiteSpace(myGuid.Guid)) throw new ArgumentNullException(nameof(myGuid));

        var guidToUpdate = _guidList.SingleOrDefault(g => g.Guid == myGuid.Guid);

        if (guidToUpdate is null) throw new ArgumentException("Unable to find the supplied guid.");

        if (myGuid.Expire.HasValue) guidToUpdate.Expire = myGuid.Expire;
        
        if (!string.IsNullOrEmpty(myGuid.User)) guidToUpdate.User = myGuid.User;
        
        return guidToUpdate;
    }

}
