using System;

///<summary>
/// class Testimonial
/// represents a testimonial
///</summary>
public class Testimonial
{
    private readonly string _id;
    private string _content;
    private string _imageAltText;
    private string _imageUrl;
    private string _testifier;
    private string _testifierCompany;
    private string _title;


    public Testimonial(
        string id,
        bool visible,
        string title,
        DateTime date,
        string content,
        string testifier
        )
    {
        // exceptions thrown if reqd attributes are missing
        //
        if (String.IsNullOrEmpty(id)) throw new ArgumentException(Messages.TestimonialsIdUndefined);
        if (String.IsNullOrEmpty(title)) throw new ArgumentException(Messages.TestimonialsTitleUndefined);
        if (String.IsNullOrEmpty(content)) throw new ArgumentException(Messages.TestimonialsContentUndefined);
        if (String.IsNullOrEmpty(testifier)) throw new ArgumentException(Messages.TestimonialsTestifierUndefined);
        if (date.Equals(null)) throw new ArgumentException(Messages.TestimonialsDateUndefined);

        _id = id;
        Visible = visible;
        _title = title;
        Date = date;
        _content = content;
        _testifier = testifier;
    }

    public string Id
    {
        get { return String.IsNullOrEmpty(_id) ? String.Empty : _id; }
    }


    public bool Visible { get; set; }


    public string Title
    {
        get { return String.IsNullOrEmpty(_title) ? String.Empty : _title; }
        set
        {
            if (String.IsNullOrEmpty(value))
                throw new InvalidOperationException(Messages.TitleUndefined);
            _title = value;
        }
    }


    public DateTime Date { get; set; }

    public string Content
    {
        get { return String.IsNullOrEmpty(_content) ? String.Empty : _content; }
        set
        {
            if (String.IsNullOrEmpty(value))
                throw new InvalidOperationException(Messages.ContentIsNull);
            _content = value;
        }
    }

    public string Testifier
    {
        get { return String.IsNullOrEmpty(_testifier) ? String.Empty : _testifier; }
        set { _testifier = value; }
    }

    public string TestifierCompany
    {
        get { return String.IsNullOrEmpty(_testifierCompany) ? String.Empty : _testifierCompany; }
        set { _testifierCompany = value; }
    }

    public string ImageUrl
    {
        get { return String.IsNullOrEmpty(_imageUrl) ? String.Empty : _imageUrl; }
        set { _imageUrl = value; }
    }

    public string ImageAltText
    {
        get { return String.IsNullOrEmpty(_imageAltText) ? String.Empty : _imageAltText; }
        set { _imageAltText = value; }
    }
}