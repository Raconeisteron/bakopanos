using System.Collections.Generic;
using System.Configuration.Provider;

/// <summary>
/// base data access class
/// </summary>
public abstract class PeopleProvider : ProviderBase
{
    public abstract List<Person> GetAllPersons();
}