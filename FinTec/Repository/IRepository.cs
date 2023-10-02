namespace Repository
{
    public interface IRepository <T>
    {
        T Add (T oneElement);

        T? Update (T updateEntity);

        void Delete (int id);

        T? Find(Func<T,bool> filter);

        IList<T> FindAll ();

    }
}