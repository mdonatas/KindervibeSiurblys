public class PhotosResultRoot
{
    public int count { get; set; }
    public object next { get; set; }
    public object previous { get; set; }
    public List<PhotosResult> results { get; set; }
}
