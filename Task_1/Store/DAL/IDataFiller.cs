namespace Store.DAL
{
    public interface IDataFiller
    {
        void Fill(ICrudRepository repository);
    }
}