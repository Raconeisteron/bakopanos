using System.Collections.Generic;
using System.Configuration.Provider;

/// <summary>
/// base data access class
/// </summary>
public abstract class TestimonialProvider : ProviderBase
{
    public abstract List<Testimonial> GetAllTestimonials();
}