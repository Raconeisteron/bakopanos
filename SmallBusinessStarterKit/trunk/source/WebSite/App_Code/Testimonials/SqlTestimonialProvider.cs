using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Configuration;

///<summary>
/// Testimonials data Provider for SQL database 
///</summary>
public class SqlTestimonialProvider : TestimonialProvider
{
    private static string connectionString()
    {
        var sec = (ConfigurationManager.GetSection("SmallBusinessDataProviders")) as SmallBusinessDataProvidersSection;
        string connectionStringName =
            sec.TestimonialsProviders[sec.TestimonialsProviderName].Parameters["connectionStringName"];
        return WebConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
    }

    public override List<Testimonial> GetAllTestimonials()
    {
        // connect to the database
        var list = new List<Testimonial>();

        using (var con = new SqlConnection(connectionString()))
        {
            con.Open();
            var cmd = new SqlCommand("GetTestimonials", con);
            SqlDataReader r = cmd.ExecuteReader();

            while (r.Read())
            {
                string imageAltText = r["imageAltText"] == DBNull.Value ? "" : (string) r["imageAltText"];

                if (r["id"] is DBNull ||
                    r["visible"] is DBNull ||
                    r["title"] is DBNull ||
                    r["date"] is DBNull ||
                    r["content"] is DBNull ||
                    r["testifier"] is DBNull)
                    throw new InvalidOperationException(Messages.TestimonialsRequiredAttributesMissing);

                var curr = new Testimonial((string) r["id"], (bool) r["visible"], (string) r["title"],
                                           (DateTime) r["date"],
                                           (string) r["content"], (string) r["testifier"])
                               {
                                   TestifierCompany = ((r["testifierCompany"] is DBNull)
                                                           ? String.Empty
                                                           : (string) r["testifierCompany"]),
                                   ImageUrl = ((r["imageUrl"] is DBNull) ? String.Empty : (string) r["imageUrl"]),
                                   ImageAltText =
                                       ((r["imageAltText"] is DBNull) ? String.Empty : (string) r["imageAltText"])
                               };

                list.Add(curr);
            }
        }

        return list;
    }
} // end class