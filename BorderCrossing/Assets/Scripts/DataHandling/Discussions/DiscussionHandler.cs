public class DiscussionHandler : BaseHandler
{
    protected override void OnDeleteData(int id)
    {
        DataPersistenceManager.Instance.OnDeleteDiscussionSave(id);
    }

    protected override void UseData(int id)
    {
        DataPersistenceManager.Instance.OnSelectTheDiscussionSave(id);
    }

}