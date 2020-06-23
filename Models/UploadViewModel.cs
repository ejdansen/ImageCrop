using Microsoft.AspNetCore.Http;

public class UploadViewModel 
{
   public string shape { set;get; }
   public double scale { set;get;}
   public IFormFile MyImage { set; get; }
}