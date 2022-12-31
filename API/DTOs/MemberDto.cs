namespace API.DTOs;

public class MemberDto
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public int Age { get; set; }
    public DateTime BirthDate { get; set; }
    public string KnownAs { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime LastActive { get; set; } = DateTime.Now;
    public string Gender { get; set; }
    public string Introduction { get; set; }
    public string LookingFor { get; set; }
    public string Interests { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public ICollection<PhotoDto> Photos { get; set; }
}

public class PhotoDto
{
    public int Id { get; set; }
    public string Url { get; set; }
    public bool IsMain { get; set; }
}