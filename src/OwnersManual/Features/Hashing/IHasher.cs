namespace OwnersManual.Features.Hashing
{
    public interface IHasher
    {
        string Hash(string clearText);
    }
}