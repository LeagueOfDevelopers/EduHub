namespace EduHubLibrary.Domain
{
    public interface IKeysRepository
    {
        void AddKey(Key key);
        Key GetKey(int keyId);
        void UpdateKey(Key key);
    }
}